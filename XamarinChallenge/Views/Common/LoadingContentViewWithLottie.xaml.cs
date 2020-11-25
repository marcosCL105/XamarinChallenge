using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinChallenge.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingContentViewWithLottie : ContentView
    {
        public static readonly BindableProperty PlayLoadingProperty = BindableProperty.Create(nameof(PlayLoading), typeof(bool), typeof(LoadingContentViewWithLottie), false, BindingMode.OneWay, null, delegate (BindableObject bindable, object oldvalue, object newvalue)
        {
            ((LoadingContentViewWithLottie)bindable).LottiePlayStateChanged((bool)oldvalue, (bool)newvalue);
        });
        public static readonly BindableProperty OkAlertProperty = BindableProperty.Create(nameof(OkAlert), typeof(bool), typeof(LoadingContentViewWithLottie), false, BindingMode.OneWay, null, delegate (BindableObject bindable, object oldvalue, object newvalue)
        {
            ((LoadingContentViewWithLottie)bindable).LottieSuccessStateChanged((bool)oldvalue, (bool)newvalue);
        });

        public bool PlayLoading
        {
            get => (bool)GetValue(PlayLoadingProperty);
            set => SetValue(PlayLoadingProperty, value);
        }
        public bool OkAlert
        {
            get => (bool)GetValue(OkAlertProperty);
            set => SetValue(OkAlertProperty, value);
        }

        public LoadingContentViewWithLottie()
        {
            InitializeComponent();

        }

        private void LoaddingLotie_OnFinish(object sender, EventArgs e)
        {
            if (OkAlert)
            {
                loadingAnimation.Animation = "loading_ok_lottie.json";
                loadingAnimation.PlayProgressSegment(0f, 1f);
                OkAlert = false;
                return;
            }
            IsVisible = false;
        }

        private void LottiePlayStateChanged(bool _oldValue, bool _newValue)
        {
            if (_newValue)
            {
                loadingAnimation.OnFinish += LoaddingLotie_OnFinish;
                loadingAnimation.Animation = "loading_lottie.json";
                loadingAnimation.Loop = true;
                loadingAnimation.PlayProgressSegment(0f, 1f);
                loadingAnimation.Play();
                IsVisible = true;
            }
            else
            {
                if (!OkAlert)
                {
                    loadingAnimation.OnFinish -= LoaddingLotie_OnFinish;
                    IsVisible = false;
                }
                loadingAnimation.Loop = false;
            }
        }

        private void LottieSuccessStateChanged(bool _oldValue, bool _newValue)
        {
            //AppConfigurations.LogDebugError("Valores de animación " + _oldValue + " " + _newValue);
        }
    }
}
