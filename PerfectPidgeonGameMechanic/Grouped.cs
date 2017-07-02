﻿using PerfectPidgeon.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerfectPidgeonGameMechanic
{
    public class Grouped : Enemy
    {
        private int _DependantInvincibility;
        private List<Vertex> _Offsets;
        private List<Enemy> _Auxes;
        private List<Enemy> _AuxCandidates;
        private List<double> _Facings;
        private GroupVariant _Active;
        private List<GroupVariant> _Variants;
        public List<Vertex> Offsets
        { get => _Offsets; }
        public List<Enemy> Auxes
        {
            get
            {
                return _Auxes;
            }
        }
        public List<Enemy> AuxCandidates { get => _AuxCandidates; set => _AuxCandidates = value; }
        public List<double> Facings { get => _Facings; set => _Facings = value; }
        public int DependantInvincibility { get => _DependantInvincibility; set => _DependantInvincibility = value; }
        public GroupVariant Active { get => _Active; set => _Active = value; }
        public List<GroupVariant> Variants { get => _Variants; set => _Variants = value; }
        public Grouped() : base()
        {
            this.Type = EnemyType.Grouped;
            this._DependantInvincibility = -1;
            this._Facings = new List<double>();
            this._Auxes = new List<Enemy>();
            this._Offsets = new List<Vertex>();
            this._Active = null;
            this._Variants = new List<GroupVariant>();
            this._AuxCandidates = new List<Enemy>();
        }
        public Grouped(Grouped Old) : base(Old)
        {
            this._DependantInvincibility = Old._DependantInvincibility;
            this._Facings = new List<double>();
            for (int i = 0; i < Old._Facings.Count; i++) this._Facings.Add(Old._Facings[i]);
            this._Auxes = new List<Enemy>();
            for (int i = 0; i < Old._Auxes.Count; i++) this._Auxes.Add(new Enemy(Old.Auxes[i]));
            this._Offsets = new List<Vertex>();
            for (int i = 0; i < Old._Offsets.Count; i++) this._Offsets.Add(new Vertex(Old._Offsets[i]));
            if(Old._Active != null) this._Active = new GroupVariant(Old._Active);
            this._Variants = new List<GroupVariant>();
            for (int i = 0; i < Old._Variants.Count; i++) this._Variants.Add(new GroupVariant(Old._Variants[i]));
            this._AuxCandidates = new List<Enemy>();
        }
        public void AddAux(Enemy Aux, Vertex Offset, double Angle)
        {
            Aux.Type = EnemyType.Aux;
            Aux.Location = Offset;
            this.Offsets.Add(Offset);
            this.Auxes.Add(Aux);
            this.Facings.Add(Angle);
        }
        public bool TryFindVariant(Enemy AuxCandidate)
        {
            if(Active == null)
            {
                List<GroupVariant> PossibleVariants = new List<GroupVariant>();
                for (int i = 0; i < this._Variants.Count; i++)
                {
                    for (int j = 0; j < this._Variants[i].Entries.Count; j++)
                    {
                        if(this._Variants[i].Entries[j].DesiredID == AuxCandidate.ID)
                        {
                            PossibleVariants.Add(this._Variants[i]);
                        }
                    }
                }
                if(PossibleVariants.Count > 0)
                {
                    Random R = new Random();
                    int Choice = R.Next(0, PossibleVariants.Count);
                    Active = PossibleVariants[Choice];
                }
            }
            if(Active != null)
            {
                for (int i = 0; i < this._Active.Entries.Count; i++)
                {
                    if (!this._Active.Entries[i].Filled && this._Active.Entries[i].DesiredID == AuxCandidate.ID)
                    {
                        this._AuxCandidates.Add(AuxCandidate);
                        this._Active.Entries[i].Filled = true;
                        AuxBehaviour AB = (AuxBehaviour)AuxCandidate.Behave;
                        AB.MergeTarget = this;
                        AB.Angle = this._Active.Entries[i].Angle;
                        AB.Offset = this._Active.Entries[i].Offset;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Attach(Enemy Aux, Vertex Offset, double Angle)
        {
            if (Math.Abs(Aux.Location.X - (this.Location.X + Offset.X)) < 10 && Math.Abs(Aux.Location.Y - (this.Location.Y + Offset.Y)) < 10)
            {
                AddAux(new Enemy(Aux), Offset, Angle);
                return true;
            }
            return false;
        }
        public override double CalculateSpeed()
        {
            double NewSpeed = 0;
            int SpeedModifiers = 1;
            NewSpeed += this.ActiveSpeed;
            for(int i = 0; i < this.Auxes.Count; i++)
            {
                if(Auxes[i] != null)
                {
                    NewSpeed += Auxes[i].ActiveSpeed;
                    SpeedModifiers++;
                }
            }
            return NewSpeed / SpeedModifiers;
        }
    }
    public class GroupVariant
    {
        private List<GroupVariantEntry> _Entries;
        public List<GroupVariantEntry> Entries { get => _Entries; set => _Entries = value; }
        public GroupVariant()
        {
            this._Entries = new List<GroupVariantEntry>();
        }
        public GroupVariant(GroupVariant Old)
        {
            this._Entries = new List<GroupVariantEntry>();
            for(int i = 0; i < Old._Entries.Count; i++)
            {
                this._Entries.Add(new GroupVariantEntry(Old._Entries[i]));
            }
        }
    }
    public class GroupVariantEntry
    {
        private bool _Filled;
        private double _Angle;
        private string _DesiredID;
        private Vertex _Offset;
        public bool Filled { get => _Filled; set => _Filled = value; }
        public string DesiredID { get => _DesiredID; set => _DesiredID = value; }
        public Vertex Offset { get => _Offset; set => _Offset = value; }
        public double Angle { get => _Angle; set => _Angle = value; }
        public GroupVariantEntry()
        {
            this._Filled = false;
            this._DesiredID = "";
            this._Angle = 0;
            this._Offset = new Vertex();
        }
        public GroupVariantEntry(GroupVariantEntry Old)
        {
            this.Filled = Old._Filled;
            this._Angle = Old._Angle;
            this._DesiredID = Old._DesiredID;
            this._Offset = Old._Offset;
        }
    }
}
