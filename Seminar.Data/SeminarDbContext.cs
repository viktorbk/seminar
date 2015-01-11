using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Data.Entities;

namespace Seminar.Business.Api.Models
{
	public class SeminarDbContext : DbContext
	{
		public DbSet<Kurs> Kurs { get; set; }
		public DbSet<Kursdeltaker> Kursdeltakere { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			// Load mappings from assembly
			modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
		}

        public class SeminarDbInitializer : CreateDatabaseIfNotExists<SeminarDbContext>
        {
            protected override void Seed(SeminarDbContext context)
            {
                base.Seed(context);
            }
        }

		public SeminarDbContext() : base("Seminar")
		{
            Database.SetInitializer<SeminarDbContext>(new SeminarDbInitializer());
		}

		public override int SaveChanges()
		{
			try
			{
				return base.SaveChanges();
			}
			catch (DbUpdateException e)
			{
				throw;
			}
			catch (DbEntityValidationException e)
			{
				var message = "";
				foreach (var eve in e.EntityValidationErrors)
				{
					var line = string.Format(CultureInfo.CurrentCulture,
						"Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					message += line + "\r\n";
					Console.WriteLine(line);

					foreach (var ve in eve.ValidationErrors)
					{
						line = string.Format(CultureInfo.CurrentCulture, "- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
						message += line + "\r\n";
						Console.WriteLine(line);
					}
				}
				throw new DbEntityValidationException(message, e);
			}
		}

	}
}
