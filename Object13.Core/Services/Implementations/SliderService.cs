using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Object13.Core.Services.Interfaces;
using Object13.Core.Utilites.Common;
using Object13.DataLayer.Models.SiteUtilites;
using Object13.DataLayer.Repository;

namespace Object13.Core.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region Ctor
        private readonly IGenericRepository<Slider> _slideRepository;
        public SliderService(IGenericRepository<Slider> slideRepository)
        {
            _slideRepository = slideRepository;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _slideRepository?.Dispose();
        }
        #endregion

        #region User
        public async Task<List<Slider>> GetAllSliders()
        {
            return await _slideRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<List<Slider>> GetActiveSlider()
        {
            return await _slideRepository.GetEntitiesQuery().Where(s => !s.IsDelete)
                .Select(s=> new Slider
                {
                    Description = s.Description,
                    Image = PathTool.Domain + PathTool.HomeSliderImagePath + s.Image,
                    Link = s.Link
                })
                .ToListAsync();
        }

        public async Task AddSlider(Slider slider)
        {
            await _slideRepository.AddEntity(slider);
            await _slideRepository.SaveChanges();
        }

        public async Task UpdateSlider(Slider slider)
        {
            _slideRepository.UpdateEntity(slider);
            await _slideRepository.SaveChanges();
        }

        public async Task<Slider> GetSliderById(long sliderId)
        {
            return await _slideRepository.GetEntityById(sliderId);
        }
        #endregion
    }
}
