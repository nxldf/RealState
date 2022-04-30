using System;
using System.Collections.Generic;
using System.Text;

namespace DF.RealEstate.Dto
{

    public interface ILocalizedDto
    {
    }
    public interface ILocalizedDto<TLocalizedDto> : ILocalizedDto
    {
        IList<TLocalizedDto> Translations { get; set; }
    }
}
