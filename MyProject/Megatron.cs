using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Robocode;
using Robocode.Util;

namespace Calazans
{
    class Megatron : RateControlRobot
    {
        // Define se o robô está andando para frente
        Boolean paraFrente; 
        public override void Run()
        {                       
            SetColors(Color.Black, Color.Black, Color.Orange, Color.Red, Color.Green);            

            double movimento = 200;

            while (true)
            {
                // Movimento aleatório na arena
                TurnGunRight(90);
                SetAhead(movimento);
                paraFrente = true;
                SetTurnRight(90);
                SetTurnGunLeft(450);

                WaitFor(new GunTurnCompleteCondition(this));
                SetAhead(movimento);
                SetTurnRight(-90);
                SetTurnGunRight(450);

                WaitFor(new GunTurnCompleteCondition(this));
                SetBack(movimento);
                paraFrente = false;
                SetTurnRight(180);
                SetTurnGunLeft(450);

                WaitFor(new GunTurnCompleteCondition(this));
                SetBack(movimento);
                SetTurnRight(-90);
                SetTurnGunRight(450);

                WaitFor(new GunTurnCompleteCondition(this));
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            // Calcula a localização exata do robô
            double absoluteBearing = Heading + e.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);

            // Se está numa distância mais favorável para tiro, atira
            if (Math.Abs(bearingFromGun) <= 3)
            {
                TurnGunRight(bearingFromGun);
                Fire(Math.Min(3 - Math.Abs(bearingFromGun), Energy - .1));
            }
            else // Se não, atira e se move
            {
                TurnGunRight(bearingFromGun);
                Fire(1);
                ReverteDirecao();
            }

            if (bearingFromGun == 0)
            {
                Scan();
            }
        }

        public override void OnHitWall(HitWallEvent e)
        {
            ReverteDirecao();
            WaitFor(new TurnCompleteCondition(this));
        }

        public override void OnHitRobot(HitRobotEvent e)
        {
            double absoluteBearing = Heading + e.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);
            SetTurnRight(bearingFromGun);
            SetTurnGunRight(bearingFromGun);
            Fire(3);
            WaitFor(new GunTurnCompleteCondition(this));
        }

        public override void OnHitByBullet(HitByBulletEvent e)
        {
            double absoluteBearing = Heading + e.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);
            TurnGunRight(bearingFromGun);
            EsquivaTiros();
            WaitFor(new GunTurnCompleteCondition(this));
        }

        public void ReverteDirecao()
        {
            if (paraFrente)
            {
                SetBack(400);
                TurnRate = 5;
                paraFrente = false;
                WaitFor(new TurnCompleteCondition(this));
            }
            else
            {
                SetAhead(400);
                TurnRate = 5;
                paraFrente = true;
                WaitFor(new TurnCompleteCondition(this));
            }
        }

        public void EsquivaTiros()
        {
            if (paraFrente)
            {
                SetBack(400);
                SetTurnRight(90);
                TurnRate = 5;
                paraFrente = false;
                WaitFor(new TurnCompleteCondition(this));
            }
            else
            {
                SetAhead(400);
                SetTurnRight(90);
                TurnRate = 5;
                paraFrente = true;
                WaitFor(new TurnCompleteCondition(this));
            }
        }
    }
}