using Encryption.Infastructure;
using Encryption.Model;
using Encryption.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encryption.ViewModel
{
    internal class Lab4VM : BaseVM
    {
        #region поля и свойства

        Evklid _evklid = new();

        private int _m;
        public int M
        {
            get { return _m; }
            set {if (value > 0) Set(ref _m, value); }
        }

        private int _n;
        public int N
        {
            get { return _n; }
            set { if (value > 0) Set(ref _n, value); }
        }

        private int _NOD;
        public int NOD
        {
            get { return _NOD; }
            set { Set(ref _NOD, value); }
        }

        private string _NODTime;
        public string NODTime
        {
            get { return _NODTime; }
            set { Set(ref _NODTime, value); }
        }

        private string _NODTimeAdvanced;
        public string NODTimeAdvanced
        {
            get { return _NODTimeAdvanced; }
            set { Set(ref _NODTimeAdvanced, value); }
        }

        private string _NODTimeA;
        public string NODTimeA
        {
            get { return _NODTimeA; }
            set { Set(ref _NODTimeA, value); }
        }

        private int _advancedNOD;
        public int advancedNOD
        {
            get { return _advancedNOD; }
            set { Set(ref _advancedNOD, value); }
        }

        private int _AMOD;
        public int AMOD
        {
            get { return _AMOD; }
            set { Set(ref _AMOD, value); }
        }

        #endregion

        #region комманды

        #region Вычислить NOD

        public ICommand GetCalculateNODCommand { get; }
        private bool CanODecryptCommand(object p)
        {
            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OnDecryptCommand(object p)
        {
            if (M > 0 && N > 0)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                
                NOD = _evklid.CalculateNOD(M, N);
                
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                NODTime = ts.ToString();
                
                Stopwatch stopWatch1 = new Stopwatch();
                stopWatch1.Start();
                
                advancedNOD = _evklid.CalculateUVT(M, N);
                
                stopWatch1.Stop();
                TimeSpan ts1 = stopWatch1.Elapsed;
                NODTimeAdvanced = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts1.Hours, ts1.Minutes, ts1.Seconds,
                    ts1.Milliseconds / 10);


                Stopwatch stopWatch2 = new Stopwatch();
                stopWatch2.Start();

                AMOD = _evklid.CalculateA(M, N);

                stopWatch2.Stop();
                TimeSpan ts2 = stopWatch2.Elapsed;
                NODTimeA = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts2.Hours, ts2.Minutes, ts2.Seconds,
                    ts2.Milliseconds / 10);
            } 
        }

        #endregion
        
        #endregion

        public Lab4VM()
        {
            GetCalculateNODCommand = new LambdaCommand(OnDecryptCommand, CanODecryptCommand);
        }

    }
}
