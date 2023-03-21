using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Entities;
namespace Zamazimah.Data.DatabaseConfiguration
{
    public static class ChangeTrackerExtensions
    {
        public static void ApplyAuditInformation(this ChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries())
            {
                if (!(entry.Entity is BaseAuditClass baseAudit)) continue;

                switch (entry.State)
                {
                    case EntityState.Modified:
                        // baseAudit.Created = now;
                        baseAudit.Modified = DateTime.Now;
                        break;

                    case EntityState.Added:
                        baseAudit.Created = DateTime.Now;
                        baseAudit.Modified = DateTime.Now;
                        break;
                }
            }
        }
    }
}
