﻿using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Factories;
using DonationAlertsApiClient.Helpers;
using DonationAlertsApiClient.Services;
using Websocket.Client;

namespace DonationAlertsApiClient.Client.Impl;

/// <summary>
/// Main class for interacting with the Donation Alerts Api.
/// </summary>
public class DonationAlertsClient : IDonationAlertsClient
{
    private readonly IDonationAlertsApiServiceFactory _donationAlertsApiServiceFactory;
    private readonly ICentrifugoServiceFactory _centrifugoServiceFactory;
    private readonly IResponseProcessingService _responseProcessingService;
    
    public event Action<DonationAlertData> ReceivedDonationAlert = delegate { };
    public event Action<PollData> ReceivedPollUpdate = delegate { };
    public event Action<DonationGoalsData> ReceivedDonationGoalsUpdate = delegate { };
    public event Action<ReconnectionType> CentrifugoReconnectionHappened
    {
        add => _centrifugoService.ReconnectionHappened += value;
        remove => _centrifugoService.ReconnectionHappened -= value;
    }

    private IDonationAlertsApiService _donationAlertsApiService;
    private ICentrifugoService _centrifugoService;
    private UserData _userData;

    public DonationAlertsClient(IDonationAlertsApiServiceFactory donationAlertsApiServiceFactory, ICentrifugoServiceFactory centrifugoServiceFactory, 
        IResponseProcessingService responseProcessingService)
    {
        _donationAlertsApiServiceFactory = donationAlertsApiServiceFactory;
        _centrifugoServiceFactory = centrifugoServiceFactory;
        _responseProcessingService = responseProcessingService;
    }

    public async Task Initialise()
    {
        _donationAlertsApiService = _donationAlertsApiServiceFactory.CreateService();
        _userData = await _donationAlertsApiService.GetUserData();
        _centrifugoService = _centrifugoServiceFactory.CreateService(_userData.SocketConnectionToken);
        _centrifugoService.ResponseReceived += _responseProcessingService.OnResponseReceived;
        
        _responseProcessingService.ReceivedDonationAlert += OnReceivedDonationAlert;
        _responseProcessingService.ReceivedPollUpdate += OnReceivedPollUpdate;
        _responseProcessingService.ReceivedDonationGoalsUpdate += OnReceivedDonationGoalsUpdate;
    }

    public async Task Connect()
    {
        await _centrifugoService.Start();
        await _centrifugoService.Connect();
    }

#region Channel subscription methods

    public async Task SubscribeToDonationAlerts()
    {
        await SubscribeToChannel(ConstantDataHelper.DonationAlertsPrefix);
    }

    public async Task SubscribeToDonationGoals()
    {
        await SubscribeToChannel(ConstantDataHelper.DonationGoalsPrefix);
    }

    public async Task SubscribeToPolls()
    {
        await SubscribeToChannel(ConstantDataHelper.PollsPrefix);
    }

    public async Task UnsubscribeFromDonationAlerts()
    {
        await UnsubscribeFromChannel(ConstantDataHelper.DonationAlertsPrefix);
    }

    public async Task UnsubscribeFromDonationGoals()
    {
        await UnsubscribeFromChannel(ConstantDataHelper.DonationGoalsPrefix);
    }

    public async Task UnsubscribeFromPolls()
    {
        await UnsubscribeFromChannel(ConstantDataHelper.PollsPrefix);
    }

#endregion

    public async Task<DonationAlertListData> GetDonationAlertsList()
    {
        return await _donationAlertsApiService.GetDonationAlertsList();
    }

    private async Task SubscribeToChannel(string channelPrefix)
    {
        var channelsData = await _donationAlertsApiService.GetChannelsData(channelPrefix, _userData.Id, _centrifugoService.CentrifugoUserId);
        foreach (var channel in channelsData) await _centrifugoService.SubscribeToChannel(channel);
    }

    private async Task UnsubscribeFromChannel(string channelPrefix)
    {
        var channelsData = await _donationAlertsApiService.GetChannelsData(channelPrefix, _userData.Id, _centrifugoService.CentrifugoUserId);
        foreach (var channel in channelsData) await _centrifugoService.UnsubscribeFromChannel(channel.Channel);
    }

    private void OnReceivedDonationGoalsUpdate(DonationGoalsData data)
    {
        ReceivedDonationGoalsUpdate(data);
    }

    private void OnReceivedPollUpdate(PollData data)
    {
        ReceivedPollUpdate(data);
    }

    private void OnReceivedDonationAlert(DonationAlertData data)
    {
        ReceivedDonationAlert(data);
    }
}