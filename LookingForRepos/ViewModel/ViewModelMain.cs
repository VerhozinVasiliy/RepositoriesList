using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using LookingForRepos.Model;

namespace LookingForRepos.ViewModel
{
    /// <summary>
    /// основной VM класс
    /// </summary>
    public class ViewModelMain : ViewModelBase
    {
        public ViewModelMain()
        {
            IsDataGridVisible = Visibility.Hidden;
            ProgressorVisible = Visibility.Hidden;
            IsButtonEnables = true;
        }

        public delegate void ExitDelegate();
        public event ExitDelegate M_ExitEvent;

        #region Props

        private string m_ProgressorLabel;
        public string ProgressorLabel
        {
            get => m_ProgressorLabel;
            set
            {
                m_ProgressorLabel = value;
                RaisePropertyChanged(() => ProgressorLabel);
            }
        }
        
        private Visibility m_ProgressorVisible;
        public Visibility ProgressorVisible
        {
            get => m_ProgressorVisible;
            set
            {
                m_ProgressorVisible = value;
                RaisePropertyChanged(() => ProgressorVisible);
            }
        }

        private bool m_IsButtonEnables;
        public bool IsButtonEnables
        {
            get => m_IsButtonEnables;
            set
            {
                m_IsButtonEnables = value;
                RaisePropertyChanged(() => IsButtonEnables);
            }
        }

        private Visibility m_IsDataGridVisible;
        public Visibility IsDataGridVisible
        {
            get => m_IsDataGridVisible;
            set
            {
                m_IsDataGridVisible = value;
                RaisePropertyChanged(() => IsDataGridVisible);
            }
        }

        private ObservableCollection<M_Repository> m_MainRepoList = new ObservableCollection<M_Repository>();
        public ObservableCollection<M_Repository> MainRepoList
        {
            get => m_MainRepoList;
            set
            {
                m_MainRepoList = value;
                RaisePropertyChanged(() => MainRepoList);
            }
        }

        private string m_TextToFind;
        public string TextToFind
        {
            get => m_TextToFind;
            set
            {
                m_TextToFind = value;
                RaisePropertyChanged(() => TextToFind);
            }
        }
        

        #endregion

        #region Commands

        /// <summary>
        /// подгрузка значений из сети
        /// </summary>
        private RelayCommand m_LoadData;
        public ICommand LoadDataCommand
        {
            get
            {
                return m_LoadData ?? (m_LoadData = new RelayCommand(
                    param => LoadDataFunc()
                ));
            }
        }
        private async void LoadDataFunc()
        {
            if (string.IsNullOrEmpty(TextToFind))
            {
                MessageBox.Show("Введите текст поиска", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsButtonEnables = false;
            ProgressorLabel = "Читаю репозитории с сайта...";
            ProgressorVisible = Visibility.Visible;

            var loadReps = new RepositoriesFromNet(TextToFind);

            var err = "";
            try
            {
                await Task.Factory.StartNew(() => loadReps.Load());
            }
            catch (System.Net.WebException)
            {
                err = "Проблема доступа к сайту. Проверьте соединение с интернет";
            }
            catch (AggregateException)
            {
                err = "Проблема доступа к сайту. Проверьте токен подключения к сайту гитхабы в файле app.config";
            }
            finally
            {
                SetMainList(loadReps.RepositoriesList);
                IsButtonEnables = true;
                ProgressorLabel = "";
                ProgressorVisible = Visibility.Hidden;
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SetMainList(List<M_Repository> inList)
        {
            MainRepoList = new ObservableCollection<M_Repository>(inList);
            IsDataGridVisible = MainRepoList.Count > 0 ? Visibility.Visible : Visibility.Hidden;
        }


        /// <summary>
        /// закрытие окна
        /// </summary>
        private bool m_DoClose;
        public async void OnWindowClosing(object sender, CancelEventArgs e)
        {
            IsButtonEnables = false;
            ProgressorLabel = "Сохраняю в БД...";
            ProgressorVisible = Visibility.Visible;
            // сохраним значения в БД
            e.Cancel = !m_DoClose;
            if (!m_DoClose)
            {
                await SavingDBOnClose();
            }
            
        }
        private async Task SavingDBOnClose()
        {
            var saverepo = new RepoToBase(MainRepoList);
            try
            {
                await Task.Factory.StartNew(() => saverepo.Save());
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                MessageBox.Show("Ошибка в строке подключения. Внимательно проверьте данные в файле app.config",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                m_DoClose = true;
                M_ExitEvent?.Invoke();
            }
        }

        /// <summary>
        /// окно загружено - подгрузим даданные последнего поиска из БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public async void OnContentRendered(object sender, EventArgs eventArgs)
        {
            IsButtonEnables = false;
            ProgressorLabel = "Читаю данные последнего поиска из БД...";
            ProgressorVisible = Visibility.Visible;

            var errStr = "";
            var repoFromDB = new LoadingRepoFromDB();
            try
            {
                await Task.Factory.StartNew(() => repoFromDB.Load());
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                errStr = "Ошибка в строке подключения. Внимательно проверьте данные в файле app.config";
            }
            catch (NullReferenceException)
            {
                errStr = "Нарушена целостность данных в таблицах БД";
            }
            finally
            {
                SetMainList(repoFromDB.RepoList);
                IsButtonEnables = true;
                ProgressorLabel = "";
                ProgressorVisible = Visibility.Hidden;
                if (!string.IsNullOrEmpty(errStr))
                {
                    MessageBox.Show(errStr, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion
    }
}
