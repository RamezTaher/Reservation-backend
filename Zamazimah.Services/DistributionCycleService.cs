using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Zamazimah.Core.Constants;
using Zamazimah.Core.Utils;
using Zamazimah.Core.Utils.Mailing;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Helpers;
using Zamazimah.Models.DistributionCycles;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Services
{
    public class DistributionCycleService : IDistributionCycleService
    {

        private readonly IDistributionCycleRepository _distributionCycleRepository;
        private readonly IHousingContractRepository _housingContractRepository;
        private readonly IOptions<MailSettings> appSettings;
        private readonly IConfiguration _configuration;

        public DistributionCycleService(IDistributionCycleRepository distributionCycleRepository,
            IHousingContractRepository housingContractRepository,
                     IOptions<MailSettings> app, IConfiguration configuration)
        {
            _distributionCycleRepository = distributionCycleRepository;
            _housingContractRepository = housingContractRepository;
            appSettings = app;
            _configuration = configuration;
        }

        public ResultApiModel<IEnumerable<DistributionCycleModel>> GetWithPagination(DistributionCycleFilterModel filter, ApplicationUser user)
        {
            return this._distributionCycleRepository.GetWithPagination(filter, user);
        }
        public IEnumerable<DistributionCycle> GetNotSeenCycles(ApplicationUser user)
        {
            return this._distributionCycleRepository.Get(x => !x.Seen && x.DistributorId == user.Id);
        }
        public DistributionCycle GetById(int id)
        {
            return _distributionCycleRepository.GetById(id);
        }

        public DistributionCycle GetDetailsById(int id)
        {
            return _distributionCycleRepository.GetDetailsById(id);
        }

        public int Create(CreateDistributionCycleModel model)
        {
            var DistributionCycle = new DistributionCycle
            {
                DistributionCycleNumber = _distributionCycleRepository.GenerateDistributionCycleNumber(),
                DistributorId = model.DistributorId,
                DriverId = model.DriverId,
                VehicleId = model.VehicleId,
                DistributionDate = model.DistributionDate,
                Status = DistributionCycleStatus.New,
                StoreId = model.StoreId,
                DistributionCycleHousingContracts = model.HounsingContracts.Select(x => new DistributionCycleHousingContract
                {
                    HousingContractId = x.HousingContractId,
                    Quantity = x.Quantity,
                    NumberOfDays = x.NumberOfDays,
                    NumberOfPilgrims = _housingContractRepository.GetById(x.HousingContractId).PilgrimsNumber,
                }).ToList(),
            };
            _distributionCycleRepository.Insert(DistributionCycle);
            _distributionCycleRepository.SaveChanges();

            // var housingContracts = _housingContractRepository.Get(k=> model.HounsingContracts.Any(a=>a.HousingContractId == k.Id));

            var newDistributionCycle = _distributionCycleRepository.GetDetailsById(DistributionCycle.Id);
            // send email to distributor
            try
            {
                var body = $"<bdi>عزيزي " + newDistributionCycle.Distributor.FullName + "  :</bdi>" + "<br/>" +
                    "<bdi>لقد تم جدولة دورة توزيع لكم بالمعطيات التالية:" + " </bdi>" + "<br/>" +
                    "<bdi>" + "تاريخ التوزيع: " + model.DistributionDate.ToString("yyyy-MM-dd") + "</bdi>" + "<br/>" +
                    "<bdi>" + " المستودع: " + newDistributionCycle.Store.StoreNumber + " - " + newDistributionCycle.Store.Title + "</bdi>" + "<br/>" +
                    "<bdi>" + " السائق: " + newDistributionCycle.Driver.FullName + "</bdi>" + "<br/>" +
                    "<bdi>" + " المركبة: " + newDistributionCycle.Vehicle.PlateNumber + "</bdi>" + "<br/>" +
                    "<bdi>" + " الكمية: " + model.HounsingContracts.Sum(d => d.Quantity) + "</bdi>" + "<br/>" +
                    "<bdi>" + " عقود السكن : " + "</bdi>" + "<br/>";
                newDistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
                {
                    body = body + "<bdi>" + " رقم العقد: " + x.HousingContract.Code + "</bdi>" + "<br/>";
                    body = body + "<bdi>" + "  الكمية: " + x.Quantity + "</bdi>" + "<br/>";
                });

                MailSettings mailSettings = appSettings.Value;
                mailSettings.EnableSsl = false;
                MessageModel message = new MessageModel
                {
                    Subject = "منصة الزمازمة - جدولة",
                    Body = body,
                    To = new List<string> { newDistributionCycle.Distributor.Email, "a.bokhari@hulool.com" },
                    From = mailSettings.Sender,
                };
                SMTPMailSender.SendMail(message, mailSettings);
            }
            catch (Exception ex)
            {

            }

            // send email to store responsable
            try
            {


                var body = $"<bdi>عزيزي " + newDistributionCycle.Store.ResponsableFirstName + " " + newDistributionCycle.Store.ResponsableLastName + "  :</bdi>" + "<br/>" +
                    "<bdi>لقد تم جدولة دورة توزيع لكم بالمعطيات التالية:" + " </bdi>" + "<br/>" +
                    "<bdi>" + "تاريخ التوزيع: " + model.DistributionDate.ToString("yyyy-MM-dd") + "</bdi>" + "<br/>" +
                    "<bdi>" + " المستودع: " + newDistributionCycle.Store.StoreNumber + " - " + newDistributionCycle.Store.Title + "</bdi>" + "<br/>" +
                    "<bdi>" + " السائق: " + newDistributionCycle.Driver.FullName + "</bdi>" + "<br/>" +
                    "<bdi>" + " المركبة: " + newDistributionCycle.Vehicle.PlateNumber + "</bdi>" + "<br/>" +
                    "<bdi>" + " عقود السكن : " + "</bdi>" + "<br/>";
                newDistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
                {
                    body = body + "<bdi>" + " رقم العقد: " + x.HousingContract.Code + "</bdi>" + "<br/>";
                    body = body + "<bdi>" + "  الكمية: " + x.Quantity + "</bdi>" + "<br/>";
                });


                MailSettings mailSettings = appSettings.Value;
                mailSettings.EnableSsl = false;
                MessageModel message = new MessageModel
                {
                    Subject = "منصة الزمازمة - جدولة",
                    Body = body,
                    To = new List<string> { newDistributionCycle.Store.ResponsableEmail },
                    From = mailSettings.Sender,
                };
                SMTPMailSender.SendMail(message, mailSettings);
            }
            catch (Exception ex)
            {

            }
            // send sms to distributor
            try
            {
                if (newDistributionCycle.Distributor.Phone != null && (newDistributionCycle.Distributor.Phone.StartsWith("966")))
                {
                    string sms_message = "";
                    sms_message = $"عزيزي " + newDistributionCycle.Distributor.FullName + "  :" + "\n" +
                    "لقد تم جدولة دورة توزيع لكم بالمعطيات التالية:" + "\n" +
                    "تاريخ التوزيع: " + model.DistributionDate.ToString("yyyy-MM-dd") + "\n" +
                    " المستودع: " + newDistributionCycle.Store.StoreNumber + " - " + newDistributionCycle.Store.Title + "\n" +
                    " السائق: " + newDistributionCycle.Driver.FullName + "\n" +
                    " المركبة: " + newDistributionCycle.Vehicle.PlateNumber + "\n" +
                    "عقود السكن : " + "\n";
                    newDistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
                    {
                        sms_message = sms_message + " رقم العقد: " + x.HousingContract.Code + "\n";
                        sms_message = sms_message + "  الكمية: " + x.Quantity + "\n";
                    });
                    SMSHelper.SendSMS(_configuration["SMS_MODE"], sms_message, newDistributionCycle.Distributor.Phone);
                }
            }
            catch (Exception e)
            {

            }

            // send sms to store responsable
            try
            {
                if (newDistributionCycle.Store.ResponsablePhone != null && (newDistributionCycle.Store.ResponsablePhone.StartsWith("966")))
                {
                    string sms_message = "";
                    sms_message = $"عزيزي " + newDistributionCycle.Store.ResponsableFirstName + " " + newDistributionCycle.Store.ResponsableLastName + "  :" + "\n" +
                    "لقد تم جدولة دورة توزيع لكم بالمعطيات التالية:" + "\n" +
                    "تاريخ التوزيع: " + model.DistributionDate.ToString("yyyy-MM-dd") + "\n" +
                    " المستودع: " + newDistributionCycle.Store.StoreNumber + " - " + newDistributionCycle.Store.Title + "\n" +
                    " السائق: " + newDistributionCycle.Driver.FullName + "\n" +
                    " المركبة: " + newDistributionCycle.Vehicle.PlateNumber + "\n" +
                    "عقود السكن : " + "\n";
                    newDistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
                    {
                        sms_message = sms_message + " رقم العقد: " + x.HousingContract.Code + "\n";
                        sms_message = sms_message + "  الكمية: " + x.Quantity + "\n";
                    });
                    SMSHelper.SendSMS(_configuration["SMS_MODE"], sms_message, newDistributionCycle.Store.ResponsablePhone);
                }
            }
            catch (Exception e)
            {

            }

            return DistributionCycle.Id;
        }

        public void Update(DistributionCycle oldDistributionCycle, CreateDistributionCycleModel model)
        {
            oldDistributionCycle.DistributorId = model.DistributorId;
            oldDistributionCycle.DriverId = model.DriverId;
            oldDistributionCycle.VehicleId = model.VehicleId;
            oldDistributionCycle.DistributionDate = model.DistributionDate;
            oldDistributionCycle.StoreId = model.StoreId;
            _distributionCycleRepository.SaveChanges();
        }
        public void MarkCycleSeen(DistributionCycle oldDistributionCycle)
        {
            oldDistributionCycle.Seen = true;
            _distributionCycleRepository.SaveChanges();
        }
        public void MarkCycleCompleted(DistributionCycle oldDistributionCycle)
        {
            oldDistributionCycle.Status = DistributionCycleStatus.Completed;
            if (oldDistributionCycle.StartCycleDate == null)
            {
                oldDistributionCycle.StartCycleDate = oldDistributionCycle.DistributionDate;
            }
            if (oldDistributionCycle.EndCycleDate == null)
            {
                oldDistributionCycle.EndCycleDate = oldDistributionCycle.StartCycleDate == null ? oldDistributionCycle.DistributionDate : oldDistributionCycle.StartCycleDate;
            }
            oldDistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
            {
                x.IsDistributionCodeAccepted = true;
            });
            _distributionCycleRepository.SaveChanges();
        }

        public void Delete(DistributionCycle DistributionCycle)
        {
            _distributionCycleRepository.Remove(DistributionCycle);
            _distributionCycleRepository.SaveChanges();
        }

        public void Start(DistributionCycle oldDistributionCycle)
        {
            oldDistributionCycle.Status = DistributionCycleStatus.Planned;
            oldDistributionCycle.StartCycleDate = DateTime.Now;
            oldDistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
            {
                if (string.IsNullOrEmpty(x.DistributionCode))
                {
                    var distributionCode = VerificationCodeHelper.GenerateRandomDigits();
                    x.DistributionCode = distributionCode;

                    List<string> emails = new List<string>();
                    List<string> bccEmails = new List<string>();
                    bccEmails.Add("m.mahdi@hulool.com");
                    bccEmails.Add("rayanzamzami@zamazemah.com.sa");
                    string responsable_phone = "";
                    string responsable_lang = "ar";
                    if (x.HousingContract.Responsable != null)
                    {
                        emails.Add(x.HousingContract.Responsable.Email);
                        responsable_phone = x.HousingContract.Responsable.Phone;
                        responsable_lang = x.HousingContract.Responsable.Lang;
                    }

                    this.SendDistributionCodeToResponsable(emails, bccEmails, oldDistributionCycle.Distributor, x.Quantity, distributionCode, responsable_phone, responsable_lang, x.HousingContract.Code);
                }
            });
            _distributionCycleRepository.SaveChanges();
        }

        public void ResendDistributionCode(DistributionCycle oldDistributionCycle, ResendDistributionCodeModel model)
        {
            oldDistributionCycle.DistributionCycleHousingContracts.Where(x => x.HousingContractId == model.HousingContractId).ToList().ForEach(x =>
            {
                if (x.DistributionCode != null)
                {
                    List<string> emails = new List<string>();
                    List<string> bccEmails = new List<string>();
                    bccEmails.Add("m.mahdi@hulool.com");
                    bccEmails.Add("rayanzamzami@zamazemah.com.sa");
                    string responsable_phone = "";
                    string responsable_lang = "ar";
                    if (x.HousingContract.Responsable != null)
                    {
                        emails.Add(x.HousingContract.Responsable.Email);
                        responsable_phone = x.HousingContract.Responsable.Phone;
                        responsable_lang = x.HousingContract.Responsable.Lang;
                    }

                    this.SendDistributionCodeToResponsable(emails, bccEmails, oldDistributionCycle.Distributor, x.Quantity, x.DistributionCode, responsable_phone, responsable_lang, x.HousingContract.Code);
                }
            });
        }

        private void SendDistributionCodeToResponsable(List<string> to, List<string> bcc, ApplicationUser distributor, int quantity, string code, string phone, string responsable_lang, string contractCode)
        {
            // send email
            try
            {
                MailSettings mailSettings = appSettings.Value;
                mailSettings.EnableSsl = false;
                string body = $"عزيزي مندوب الاستلام لعقد السكن " + contractCode + "<br/>" +
                 "<bdi>مندوبنا في الطريق اليك >> :</bdi>" + "<br/>" +
                  "<bdi>اسم المندوب : " + "</bdi>" + "<br/>" +
                      distributor.FullName + "<br/>" +
                  "<bdi>جوال المندوب : " + "</bdi>" + "<br/>" +
                  distributor.Phone + "<br/>" +
                  "<bdi>عدد الكراتين : " + "</bdi>" + "<br/>" +
                  quantity + "<br/>" +
                  "<bdi>رمز التسليم : " + "</bdi>" + "<br/>"
                   + code;
                string title = "منصة الزمازمة - التسليم";

                if (responsable_lang == "en")
                {
                    body = $"Dear Customer in House Contract " + contractCode + "<br/>" +
                     "Our zamzam deliverey Rep. is on his way to you >>>>" + "<br/>" +
                    "Mr. " + distributor.FullName + " <br/> " +
                    "Tel: " + distributor.Phone + "<br/>" +
                    "Number of boxes: " + quantity + "<br/>" +
                     "Delivery acceptance code: " + code + " <br/> ";

                    title = "Zamazimah platform - Deliverey";
                }


                MessageModel message = new MessageModel
                {
                    Subject = title,
                    Body = body,
                    To = to,
                    BCC = bcc,
                    From = mailSettings.Sender,
                };
                SMTPMailSender.SendMail(message, mailSettings);
            }
            catch (Exception ex)
            {

            }
            // send sms
            try
            {
                if (phone != null && (phone.StartsWith("966")))
                {
                    string sms_message = "";
                    sms_message = $"عزيزي مندوب الاستلام لعقد السكن " + contractCode + "\n" +
                     "مندوبنا في الطريق اليك >> :" + "\n" +
                    "اسم المندوب : " + "\n" +
                    distributor.FullName + "\n" +
                    "جوال المندوب : " + "\n" +
                    distributor.Phone + "\n" +
                    "عدد الكراتين : " + "\n" +
                    quantity + "\n" +
                    "رمز التسليم : " + "\n" + code;

                    if (responsable_lang == "en")
                    {
                        sms_message = $"Dear Customer in House Contract " + contractCode + "\n" +
                         "Our zamzam deliverey Rep. is on his way to you >>>>" + "\n" +
                        "Mr. " + distributor.FullName + "\n" +
                        "Tel: " + distributor.Phone + "\n" +
                        "Number of boxes: " + quantity + "\n" +
                         "Delivery acceptance code: " + code;
                    }

                    SMSHelper.SendSMS(_configuration["SMS_MODE"], sms_message, phone);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void Accept(DistributionCycle oldDistributionCycle, AcceptDistributionCodeModel model)
        {
            var dcontract = oldDistributionCycle.DistributionCycleHousingContracts.FirstOrDefault(x => x.DistributionCode == model.DistributionCode && x.HousingContractId == model.HousingContractId);
            if (dcontract != null)
            {
                dcontract.IsDistributionCodeAccepted = true;
                dcontract.AcceptanceDate = DateTime.Now;
                if (oldDistributionCycle.DistributionCycleHousingContracts.All(x => x.IsDistributionCodeAccepted))
                {
                    oldDistributionCycle.Status = DistributionCycleStatus.Completed;
                    oldDistributionCycle.EndCycleDate = DateTime.Now;
                    this.SendReturnQuantityMessage(oldDistributionCycle);
                }

                string report_url = _configuration["WEBSITE_URL"] + "reports/distribution-report?distributionCyclesId=" + dcontract.DistributionCycleId + "&housingContractId=" + dcontract.HousingContractId;


                report_url = ShortUrlHelper.ConvertToShortUrl(report_url);
                dcontract.ReportUrl = report_url;
                // send email
                try
                {
                    List<string> emails = new List<string>();
                    if (oldDistributionCycle.Distributor != null)
                    {
                        emails.Add(oldDistributionCycle.Distributor.Email);
                    }
                    emails.Add("a.bokhari@hulool.com");
                    MailSettings mailSettings = appSettings.Value;
                    mailSettings.EnableSsl = false;
                    MessageModel message = new MessageModel
                    {
                        Subject = "منصة الزمازمة - ايصال التسليم",
                        Body = $"<html lang='ar' dir='rtl'>" + "السلام عليكم ورحمة الله وبركاته </bdi>" + "<br/>" +
                        "<bdi>مكة في  " + "</bdi>" + DateTime.Now.ToString("yyyy-MM-dd") + "<br/>" +
                        "<bdi>تم تسليم " + dcontract.Quantity + " كرتون ماء زمزم لعقد السكن رقم " + dcontract.HousingContract.Code +
                        "<br/>" + "  عن طريق الموزع : " + "<br/>" + oldDistributionCycle.Distributor.FullName + "<br/>" + "   تجدون في الرابط التالي ايصال التسليم</bdi>" + "<br/>" +
                        report_url
                        + "<p>قال رسول الله صلّى الله عليه وسلّم: (خَيرُ ماءٍ على وجْهِ الأرضِ ماءُ زَمْزَمَ ، فِيه طعامٌ من الطُّعْمِ، و شِفاءٌ من السُّقْمِ)</p>"
                        + "</body></html>",
                        To = emails,
                        From = mailSettings.Sender,
                    };
                    SMTPMailSender.SendMail(message, mailSettings);
                }
                catch (Exception ex)
                {

                }

                try
                {
                    string sms_message = $"عزيزي مندوب عقد سكن رقم " + dcontract.HousingContract.Code + "\n" +
                        "مرفق رابط سند الاستلام : " + "\n" + report_url;
                    if (dcontract.HousingContract.Responsable.Lang != "ar")
                    {
                        sms_message = $"Dear receiver for House Contract " + dcontract.HousingContract.Code + "\n" +
                            "Please find hereunder the receipt link" + "\n" + report_url;
                    }
                    SMSHelper.SendSMS(_configuration["SMS_MODE"], sms_message, dcontract.HousingContract.Responsable.Phone);
                }
                catch (Exception ex)
                {

                }

                _distributionCycleRepository.SaveChanges();
            }
        }


        private void SendReturnQuantityMessage(DistributionCycle cycle)
        {
            if (cycle.DistributionCycleHousingContracts.Any(x => x.ReturnedQuantity > 0))
            {
                // send email
                try
                {
                    MailSettings mailSettings = appSettings.Value;
                    mailSettings.EnableSsl = false;
                    string body = $"عزيزي " + cycle.Store.ResponsableFirstName + " " + cycle.Store.ResponsableLastName + " أمين المستودع " + "<br/>" +
                     "<bdi>نود ابلاغكم ان المندوب " + cycle.Distributor.FullName + " عليه كمية مرتجعة من دورة توزيع عدد " + cycle.DistributionCycleNumber + " حسب التفصيل التالي</bdi>" + "<br/>";
                    cycle.DistributionCycleHousingContracts.ToList().ForEach(contract =>
                    {
                        if (contract.ReturnedQuantity > 0)
                        {
                            body = body + "عقد السكن رقم " + contract.HousingContract.Code + " : " + "كمية مرتجعة  " + contract.ReturnedQuantity + " كرتون " + "<br/>";
                        }
                    });
                    string title = "منصة الزمازمة - إدارة الرجيع";
                    MessageModel message = new MessageModel
                    {
                        Subject = title,
                        Body = body,
                        To = new List<string> { cycle.Store.ResponsableEmail },
                        From = mailSettings.Sender,
                    };
                    SMTPMailSender.SendMail(message, mailSettings);
                }
                catch (Exception ex)
                {

                }
                // send sms
                try
                {
                    if (cycle.Store.ResponsablePhone != null && (cycle.Store.ResponsablePhone.StartsWith("966")))
                    {
                        string sms_message = "";
                        sms_message = $"عزيزي " + cycle.Store.ResponsableFirstName + " " + cycle.Store.ResponsableLastName + " أمين المستودع " + "\n" +
                         "نود ابلاغكم ان المندوب " + cycle.Distributor.FullName + " عليه كمية مرتجعة من دورة توزيع عدد " + cycle.DistributionCycleNumber + " حسب التفصيل التالي" + "\n";
                        cycle.DistributionCycleHousingContracts.ToList().ForEach(contract =>
                        {
                            if (contract.ReturnedQuantity > 0)
                            {
                                sms_message = sms_message + "عقد السكن رقم " + contract.HousingContract.Code + " : " + "كمية مرتجعة  " + contract.ReturnedQuantity + " كرتون " + "\n";
                            }
                        });

                        SMSHelper.SendSMS(_configuration["SMS_MODE"], sms_message, cycle.Store.ResponsablePhone);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }


        public void UploadDistributionImage(DistributionCycle distributionCycle, IFormFile picture)
        {
            distributionCycle.DistributionImageUrl = UploadFilesHelper.UploadPicture(picture, PicturesFolder.HousingContractsFolder);
            _housingContractRepository.SaveChanges();
        }

        public void ReturnQuantity(DistributionCycle oldDistributionCycle, ReturnQuantityModel model)
        {
            var dcontract = oldDistributionCycle.DistributionCycleHousingContracts.FirstOrDefault(x => x.HousingContractId == model.HousingContractId);
            if (dcontract != null)
            {
                dcontract.ReturnedQuantity = model.ReturnedQuantity;
                dcontract.IsReturnAccepted = true;
                if (dcontract.Quantity == model.ReturnedQuantity)
                {
                    dcontract.IsDistributionCodeAccepted = true;
                }
                if (oldDistributionCycle.DistributionCycleHousingContracts.All(x => x.IsDistributionCodeAccepted))
                {
                    oldDistributionCycle.Status = DistributionCycleStatus.Completed;
                    oldDistributionCycle.EndCycleDate = DateTime.Now;
                }
                _distributionCycleRepository.SaveChanges();
            }
        }

        public ResultApiModel<IEnumerable<DistributionCycleForReportsModel>> GetForReportsWithPagination(DistributionCycleFilterModel filter, ApplicationUser user)
        {
            return _distributionCycleRepository.GetForReportsWithPagination(filter, user);
        }
    }

    public interface IDistributionCycleService
    {
        ResultApiModel<IEnumerable<DistributionCycleModel>> GetWithPagination(DistributionCycleFilterModel filter, ApplicationUser user);
        DistributionCycle GetById(int id);
        int Create(CreateDistributionCycleModel model);
        void Update(DistributionCycle oldDistributionCycle, CreateDistributionCycleModel newModel);
        public void Delete(DistributionCycle DistributionCycle);
        void Start(DistributionCycle oldDistributionCycle);
        DistributionCycle GetDetailsById(int id);
        void Accept(DistributionCycle oldDistributionCycle, AcceptDistributionCodeModel model);
        void ResendDistributionCode(DistributionCycle oldDistributionCycle, ResendDistributionCodeModel model);
        IEnumerable<DistributionCycle> GetNotSeenCycles(ApplicationUser user);
        void MarkCycleSeen(DistributionCycle oldDistributionCycle);
        void UploadDistributionImage(DistributionCycle distributionCycle, IFormFile picture);
        void ReturnQuantity(DistributionCycle oldDistributionCycle, ReturnQuantityModel model);
        void MarkCycleCompleted(DistributionCycle oldDistributionCycle);
        ResultApiModel<IEnumerable<DistributionCycleForReportsModel>> GetForReportsWithPagination(DistributionCycleFilterModel filter, ApplicationUser user);
    }
}
