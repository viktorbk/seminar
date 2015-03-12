using System.Data.Entity.ModelConfiguration;
using Seminar.Data.Entities;

namespace Seminar.Data.Mapping
{
	public class KursMap : EntityTypeConfiguration<Kurs>
	{
		public KursMap()
		{
			HasMany(x => x.Kursdeltakere).WithRequired(x => x.Kurs).HasForeignKey(x => x.KursId);
		}
	}
}
