using Models;

namespace Views
{
    public abstract class TransformableView : ViewBase
    {
        public abstract TransformableModel TransformableModel { get; }
    }
}
