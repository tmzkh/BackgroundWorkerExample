using BGWorkerExample.Data;
using BGWorkerExample.Mvvm;
using BGWorkerExample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BGWorkerExample.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Label props

        private string _label = "qwer";
        public string Label { 
            get { return _label; }
            set { 
                _label = value;
                base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Label)));
            } 
        }

        private string _textBoxLabel = "qwer";
        public string TextBoxLabel {
            get { return _textBoxLabel; }
            set {
                _textBoxLabel = value;
                base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(TextBoxLabel)));
            }
        }

        private string _buttonLabel = "qwer";
        public string ButtonLabel {
            get { return _buttonLabel; }
            set {
                _buttonLabel = value;
                base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(ButtonLabel)));
            }
        }

        #endregion

        #region Commands

        private string _textBoxText;

        public string TextBoxText {
            get { return _textBoxText; }
            set {
                _textBoxText = TextBoxLabel = value;
                base.OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(TextBoxText)));
            }
        }

        public RelayCommand _fetchAsync;
        public ICommand FetchAsync {
            get {
                if (_fetchAsync == null) {
                    _fetchAsync = new RelayCommand(x => {
                        _fakeClassService.AddItem(new FakeClass() { Id = id++, Name = $"name {id}" });
                    }, x => true);
                }
                
                return _fetchAsync; 
            }
        }

        public RelayCommand _changeTextWithButton;
        public ICommand ChangeTextWithButton {
            get {
                if (_changeTextWithButton == null) {
                    _changeTextWithButton = new RelayCommand(x => {
                        ButtonLabel = _buttonLabel + "a";
                    }, x => true);
                }

                return _changeTextWithButton;
            }
        }

        public RelayCommand _stopProcessingCommand;
        public ICommand StopCommand {
            get {
                if (_stopProcessingCommand == null) {
                    _stopProcessingCommand = new RelayCommand(x => {
                        ((App)Application.Current).IoC.StopQueueProcessor();
                    }, x => true);
                }

                return _stopProcessingCommand;
            }
        }

        #endregion

        private int id = 0;

        private readonly FakeClassService _fakeClassService;

        public MainWindowViewModel() {
            this._fakeClassService = ((App)Application.Current).IoC.FakeClassService;
        }
    }
}
