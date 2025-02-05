using Resort.Application.Common.Interfaces;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Infrastructure.Repository
{
    public class VillaNumberRepository: Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            this._db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(VillaNumber entity)
        {
            _db.Update(entity);
        }
    }
}
