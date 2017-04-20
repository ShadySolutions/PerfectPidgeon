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
        private int _Accessory;
        private string _Title;
        private string _Next;
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
        public int Accessory
        {
            get
            {
                return _Accessory;
            }

            set
            {
                _Accessory = value;
            }
        }
        public string Title
        {
            get
            {
                return _Title;
            }

            set
            {
                _Title = value;
            }
        }
        public string Next
        {
            get
            {
                return _Next;
            }

            set
            {
                _Next = value;
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
            this._Accessory = 0;
            this._Title = "TST";
            this._Next = "";
            this._Back = new Background("", BackgroundType.Static);
            this._Enemies = new List<Enemy>();
        }
    }
}
