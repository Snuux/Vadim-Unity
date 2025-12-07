
namespace MobaLesson
{
    class CompositeController : Controller
    {
        Controller[] _controllers;

        public CompositeController(params Controller[] controllers)
        {
            _controllers = controllers;
        }

        public override void Enable()
        {
            base.Enable();

            foreach (var controller in _controllers)
                controller.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            foreach (var controller in _controllers)
                controller.Disable();
        }

        public override void UpdateLogic(float deltaTime)
        {
            foreach (var controller in _controllers)
                controller.UpdateLogic(deltaTime);
        }
    }
}