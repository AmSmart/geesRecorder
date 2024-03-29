﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Client.Server.Services
{
    public class FingerprintCommsHub : Hub
    {
        private readonly FingerprintRunner _fingerprintRunner;

        public FingerprintCommsHub(FingerprintRunner fingerprintRunner)
        {
            _fingerprintRunner = fingerprintRunner;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public void Enrol()
        {
            _fingerprintRunner.Enrol();
        }
        
        public void Search()
        {
            _fingerprintRunner.Search();
        }
        
        public void Abort()
        {
            _fingerprintRunner.Abort();
        }
    }
}
