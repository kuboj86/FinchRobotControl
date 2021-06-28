using FinchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinchRobotControl
{
    class TalentShow
    {
        public static void TalentShowDisplayLightAndSound(Finch finchRobot, int TurnSpeed)
        {
            finchRobot.setMotors(-TurnSpeed, TurnSpeed);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1396); //F
            finchRobot.wait(220);
            finchRobot.noteOn(1479);//F#
            finchRobot.wait(220);
            finchRobot.setLED(0, 0, 0);


            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1567); // G
            finchRobot.wait(330);
            finchRobot.noteOn(1661); //G#
            finchRobot.wait(330);
            finchRobot.setLED(0, 0, 0);

            finchRobot.setMotors(TurnSpeed,-TurnSpeed);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1760); // A
            finchRobot.wait(330);
            finchRobot.noteOn(2093); // C HIGH NOTE
            finchRobot.wait(330);
            finchRobot.setLED(0, 0, 0);

            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1975); // B
            finchRobot.wait(165);
            finchRobot.noteOn(2093); // C
            finchRobot.wait(165);
            finchRobot.noteOn(1975); // B
            finchRobot.wait(165);
            finchRobot.noteOn(1864); // A#
            finchRobot.wait(330);
            finchRobot.setLED(0, 0, 0);

            finchRobot.setMotors(-TurnSpeed, TurnSpeed);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1760); // A
            finchRobot.wait(330);
            finchRobot.noteOn(1661); // G#
            finchRobot.wait(330);
            finchRobot.setLED(0, 0, 0);


            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1567); // G HOLD
            finchRobot.wait(330);
            finchRobot.noteOn(1479);//F#
            finchRobot.wait(165);
            finchRobot.setLED(0, 0, 0);

            finchRobot.setMotors(TurnSpeed, -TurnSpeed);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(1396); //F
            finchRobot.wait(330);
            finchRobot.noteOff();
            finchRobot.noteOn(1396); //F
            finchRobot.wait(330);
            finchRobot.noteOff();
            finchRobot.noteOn(1396); //F
            finchRobot.wait(659);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);

            finchRobot.setMotors(0, 0);
            finchRobot.noteOff();
        }
    }
}
