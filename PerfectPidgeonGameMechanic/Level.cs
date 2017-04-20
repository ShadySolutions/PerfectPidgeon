﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerfectPidgeonGameMechanic.LevelData;

namespace PerfectPidgeonGameMechanic
{
    public class Level
    {
        private int _MaxSpawns;
        private Background _Back;
        private List<Enemy> _Enemies;
        public int MaxSpawns
        {
            get
            {
                return _MaxSpawns;
            }

            set
            {
                _MaxSpawns = value;
            }
        }
        public Background Back
        {
            get
            {
                return _Back;
            }

            set
            {
                _Back = value;
            }
        }
        public List<Enemy> Enemies
        {
            get
            {
                return _Enemies;
            }

            set
            {
                _Enemies = value;
            }
        }       
        public Level()
        {
            this._MaxSpawns = 10;
            this._Back = new Background("", BackgroundType.Static);
            this._Enemies = new List<Enemy>();
        }
    }
}
