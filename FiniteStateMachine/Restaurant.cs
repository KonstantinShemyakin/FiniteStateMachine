using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FiniteStateMachine
{
    internal class Restaurant
    {
        public enum States {enter, sit, menu, food, pay, leave, exit};
        private States state;

        public Restaurant(States init_state = 0) => state = init_state;
        
        public void NextState() => state++;

        public void PrevState() => state--;

        public void ChangeState(States new_state) => state = new_state;

        public States GetState() => state;
    }

    internal class Button
    {
        private bool pressed = false;
        private bool enabled = false;
        private Rectangle position;
        int timer = 0;

        public Button(Rectangle pos)
        {
            this.position = pos;
        }

        public bool IsPressed() => pressed;
        public bool IsEnabled() => enabled;
        public void Enable() => enabled = true;
        public void Disable() 
        {
            enabled = false; 
            pressed = false;
        }
        public Rectangle GetPosition() => position;
        public void UpdateTimer()
        {
            if (timer > 0) timer--;
        }
        public void CheckPressed(int mouse_x, int mouse_y, bool mouse_press)
        {
            if (timer <= 0 && (position.Contains(mouse_x, mouse_y) && mouse_press))
            {
                pressed = true;
                timer = 40;
            }
            else pressed = false;
        }
    }
}
