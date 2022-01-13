﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesListShow.xaml
    /// </summary>
    public partial class DronesListShow : Window
    {
        BlApi.IBL bl;
        public DronesListShow(BlApi.IBL _bl) //constructor
        {
            InitializeComponent();
            DronesListView.ItemsSource = _bl.GetAllDronesToList();
            bl = _bl;

            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatuses)); //filling the comboboxes
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem == null)
            {
                DronesListView.ItemsSource = bl.GetAllDrones();
                return;
            }

            Predicate<BO.DroneToList> p;
            if (WeightSelector.SelectedIndex != -1) //if the weight selector is selected as well
                p = s => s.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex && s.Status == (BO.DroneStatuses)StatusSelector.SelectedIndex;
            else
                p = s => s.Status == (BO.DroneStatuses)StatusSelector.SelectedIndex;
            DronesListView.ItemsSource = bl.GetAllDronesToListBy(p); //fill the list view
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Predicate<BO.DroneToList> p;
            if (WeightSelector.SelectedItem == null)
            {
                DronesListView.ItemsSource = bl.GetAllDrones();
                return;
            }
            if (StatusSelector.SelectedIndex != -1) //if the status selector is selected as well
                p = s => s.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex && s.Status == (BO.DroneStatuses)StatusSelector.SelectedIndex;
            else
                p = s => s.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex;
            DronesListView.ItemsSource = bl.GetAllDronesToListBy(p);

        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneShow(bl).ShowDialog(); //opening the other window

        }


        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            BO.DroneToList d = DronesListView.SelectedItem as BO.DroneToList;
            if (d != null)
                new DroneShow(bl, bl.GetDrone(d.Id)).Show(); //opening the options widow
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Predicate<BO.DroneToList> p = null;
            if (StatusSelector.SelectedIndex != -1 && WeightSelector.SelectedIndex != -1) //if both of the combo boxes are selected
                p = s => s.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex && s.Status == (BO.DroneStatuses)StatusSelector.SelectedIndex; //create the predicate
            else if (WeightSelector.SelectedIndex != -1) //if only one is selected
                p = s => s.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex; //create the predicate
            else if (StatusSelector.SelectedIndex != -1)
                p = s => s.Status == (BO.DroneStatuses)StatusSelector.SelectedIndex;//create the predicate
            GroupBy.IsEnabled = true;
            if (p == null) //if the comboboxes werent selected
                DronesListView.ItemsSource = bl.GetAllDronesToList();
            else
                DronesListView.ItemsSource = bl.GetAllDronesToListBy(p);

        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.GetAllDronesToList();
            GroupBy.IsEnabled = true;
            StatusSelector.SelectedValue = null;
            WeightSelector.SelectedValue = null;

        }


        private void GroupByClick_1(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            view.GroupDescriptions.Add(groupDescription);
            GroupBy.IsEnabled = false;
        }
    }

}