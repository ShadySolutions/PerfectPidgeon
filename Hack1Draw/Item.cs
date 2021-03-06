﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PerfectPidgeon.Draw
{
    public class Item
    {
        private int _ArtIndex;
        private int _ImageIndex;
        private int _ImageIndexIncrement;
        private int _AngleOffsetIndex;
        private int _AngleOffsetIndexIncrement;
        private int _Other;
        private double _Size;
        private double _Facing;
        private Point _Location;
        private Color _Paint;
        public int ArtIndex
        {
            get { return _ArtIndex; }
            set { _ArtIndex = value; }
        }
        public int ImageIndex
        {
            get { return _ImageIndex; }
            set { _ImageIndex = value; }
        }
        public int ImageIndexIncrement
        {
            get { return _ImageIndexIncrement; }
            set { _ImageIndexIncrement = value; }
        }
        public int AngleOffsetIndex
        {
            get { return _AngleOffsetIndex; }
            set { _AngleOffsetIndex = value; }
        }
        public int AngleOffsetIndexIncrement
        {
            get { return _AngleOffsetIndexIncrement; }
            set { _AngleOffsetIndexIncrement = value; }
        }
        public double Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        public double Facing
        {
            get { return _Facing; }
            set { _Facing = value; }
        }
        public Point Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public int Other
        {
            get
            {
                return _Other;
            }

            set
            {
                _Other = value;
            }
        }
        public Color Paint
        {
            get
            {
                return _Paint;
            }

            set
            {
                _Paint = value;
            }
        }
        public Item(int ArtIndex, Point Location, double Facing, double Size)
        {
            this._ArtIndex = ArtIndex;
            this._ImageIndex = 0;
            this._ImageIndexIncrement = 1;
            this._AngleOffsetIndex = 0;
            this._AngleOffsetIndexIncrement = 1;
            this._Size = Size;
            this._Location = Location;
            this._Facing = Facing;
            this._Paint = Color.White;
        }
        public Item(int ArtIndex, Point Location, double Facing, double Size, int ImageIndex)
        {
            this._ArtIndex = ArtIndex;
            this._ImageIndex = ImageIndex;
            this._ImageIndexIncrement = 1;
            this._AngleOffsetIndex = 0;
            this._AngleOffsetIndexIncrement = 1;
            this._Size = Size;
            this._Location = Location;
            this._Facing = Facing;
            this._Paint = Color.White;
        }
    }
}
