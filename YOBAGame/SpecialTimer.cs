using System;

namespace YOBAGame
{
    internal class SpecialTimer
    {
        private double _curTime;
        private DateTime _lastMoment;
        private double _timeSpeed;
        private double _lastTime;

        public bool IsPaused { get; private set; }

        public double CurrentTime
        {
            get
            {
                Update();
                return _curTime;
            }
        }
        public double TimeSpeed
        {
            get { return _timeSpeed; }
            set
            {
                Update();
                _timeSpeed = value;
            }
        }

        public double LastTimeSpan()
        {
                var newTime = CurrentTime;
                var res = newTime - _lastTime;
                _lastTime = newTime;
                return res;
        }

        public SpecialTimer()
        {
            _curTime = 0.0;
            _timeSpeed = 1.0;
            _lastMoment = DateTime.Now;
            _lastTime = _curTime;
            IsPaused = true;
        }

        public void Pause()
        {
            Update();
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
            _lastMoment = DateTime.Now;
        }

        private void Update()
        {
            if (IsPaused)
                return;
            var newMoment = DateTime.Now;
            _curTime += (newMoment - _lastMoment).TotalSeconds * TimeSpeed;
            _lastMoment = newMoment;
        }
    }
}