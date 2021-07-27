using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.SiteUtilites;

namespace Object13.Core.Services.Interfaces
{
    public interface ISliderService : IDisposable
    {
        Task<List<Slider>> GetAllSliders();
        Task<List<Slider>> GetActiveSlider();
        Task AddSlider(Slider slider);
        Task UpdateSlider(Slider slider);
        Task<Slider> GetSliderById(long sliderId);
    }
}
