using System;
using System.Collections.Generic;
using System.Linq;
using Scripts._Client.Units.SingleUnit.ViewControllers;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit
{
    public interface ISingleUnitPresenter
    {
        void Update(IUnitDto unitDto);
    }

    public class SingleUnitPresenter : ISingleUnitPresenter
    {
        private readonly ICertainViewUnitObject _certainViewUnitObject;
        private readonly IUnitViewComponent[] _components;
        
        public SingleUnitPresenter(
            IUnitDto unitDto,
            Func<IUnitDto, ICertainViewUnitObject> viewUnitObjectFactory,
            Func<ICertainViewUnitObject, IEnumerable<IUnitViewComponent>> componentsFactory)
        {
            _certainViewUnitObject = viewUnitObjectFactory.Invoke(unitDto);
            _components = componentsFactory.Invoke(_certainViewUnitObject).ToArray();
        }

        public void Update(IUnitDto unitDto)
        {
            var length = _components.Length;
            for (int i = 0; i < length; i++)
            {
                _components[i].Update(unitDto);
            }
        }
    }
}