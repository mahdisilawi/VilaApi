using Microsoft.EntityFrameworkCore;
using Vila.WebApi.Context;
using Vila.WebApi.Dtos;
using Vila.WebApi.Paging;
using AutoMapper;


namespace Vila.WebApi.Services.Vila
{
    public class VilaService : IVilaService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
    
        public VilaService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Create(Models.Vila model)
        {
            _context.Vilas.Add(model);
            return Save();
        }

        public bool Delete(Models.Vila model)
        {
            _context.Vilas.Remove(model);
            return Save();
        }

        public List<Models.Vila> GetAll() =>
            _context.Vilas.ToList();

        public Models.Vila GetById(int id) =>
            _context.Vilas.FirstOrDefault(v => v.VilaId == id);

        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;

        public VilaPaging SearchVila(int pageId, string? filter, int take)
        {
            IQueryable<Models.Vila> vilas = _context.Vilas.Include(x => x.Details);
            if (!string.IsNullOrEmpty(filter))
                vilas = vilas.Where(v =>
                v.Name.Contains(filter)||
                v.State.Contains(filter)||
                v.Address.Contains(filter)||
                v.City.Contains(filter));
            VilaPaging vilaPaging = new();

            vilaPaging.Generate(vilas, pageId, take);
            vilaPaging.Filter = filter;
            vilaPaging.Vilas = new();
            int skip = (pageId - 1) * take;
            var list = vilas.Skip(skip).Take(take).ToList();
            list.ForEach(x =>
            {
                vilaPaging.Vilas.Add(_mapper.Map<VilaSearchDto>(x));
            });

            return vilaPaging;


        }

        public VilaAdminPaging SearchVilaAdmin(int pageId, string? filter, int take)
        {
            IQueryable<Models.Vila> vilas = _context.Vilas.Include(x => x.Details);
            if (!string.IsNullOrEmpty(filter))
                vilas = vilas.Where(v =>
                v.Name.Contains(filter) ||
                v.State.Contains(filter) ||
                v.Address.Contains(filter) ||
                v.City.Contains(filter));
            VilaAdminPaging vilaPaging = new();

            vilaPaging.Generate(vilas, pageId, take);
            vilaPaging.Filter = filter;
            vilaPaging.VilaDtos = new();
            int skip = (pageId - 1) * take;
            var list = vilas.Skip(skip).Take(take).ToList();
            list.ForEach(x =>
            {
                vilaPaging.VilaDtos.Add(_mapper.Map<VilaDto>(x));
            });

            return vilaPaging;


        }

        public bool Update(Models.Vila model)
        {
            _context.Vilas.Update(model);
            return Save();
        }
    }
}
