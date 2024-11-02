using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MushroomsCRUD.Data;

namespace MushroomsCRUD
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();

            LoadData();
        }

        private void LoadData()
        {
            var mushrooms = _context.Mushrooms.ToList();
            MushroomDataGrid.ItemsSource = mushrooms;

            var gills = _context.Gills.ToList();
            GillDataGrid.ItemsSource = gills;

            var stalks = _context.Stalks.ToList();
            StalkDataGrid.ItemsSource = stalks;
        }

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }

        private void DeleteMushroom_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int id = (int)button.Tag;
            var mushroom = _context.Mushrooms.Find(id);
            if (mushroom != null)
            {
                _context.Mushrooms.Remove(mushroom);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void DeleteGill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender;
                int id = (int)button.Tag;
                var gill = _context.Gills.Find(id);
                if (gill != null)
                {
                    _context.Gills.Remove(gill);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void DeleteStalk_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int id = (int)button.Tag;
            var stalk = _context.Stalks.Find(id);
            if (stalk != null)
            {
                _context.Stalks.Remove(stalk);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void AddMushroomButton_Click(object sender, RoutedEventArgs e)
        {
            var gill = new Gill
            {
                GillAttachment = GillAttachmentTextBox.Text,
                GillSpacing = GillSpacingTextBox.Text,
                GillSize = GillSizeTextBox.Text,
                GillColor = GillColorTextBox.Text
            };

            var stalk = new Stalk
            {
                StalkShape = StalkShapeTextBox.Text,
                StalkRoot = StalkRootTextBox.Text,
                StalkSurfaceAboveRing = StalkSurfaceAboveRingTextBox.Text,
                StalkSurfaceBelowRing = StalkSurfaceBelowRingTextBox.Text,
                StalkColorAboveRing = StalkColorAboveRingTextBox.Text,
                StalkColorBelowRing = StalkColorBelowRingTextBox.Text
            };

            var mushroom = new Mushroom
            {
                CapShape = CapShapeTextBox.Text,
                CapSurface = CapSurfaceTextBox.Text,
                CapColor = CapColorTextBox.Text,
                Bruises = BruisesTextBox.Text,
                Odor = OdorTextBox.Text,
                VeilType = VeilTypeTextBox.Text,
                VeilColor = VeilColorTextBox.Text,
                RingNumber = RingNumberTextBox.Text,
                RingType = RingTypeTextBox.Text,
                SporePrintColor = SporePrintColorTextBox.Text,
                Population = PopulationTextBox.Text,
                Habitat = HabitatTextBox.Text,
                Poisonous = PoisonousTextBox.Text,

                Gill = gill,
                Stalk = stalk
            };

            _context.Mushrooms.Add(mushroom);
            _context.SaveChanges();

            CapShapeTextBox.Clear();
            CapSurfaceTextBox.Clear();
            CapColorTextBox.Clear();
            BruisesTextBox.Clear();
            OdorTextBox.Clear();
            VeilTypeTextBox.Clear();
            VeilColorTextBox.Clear();
            RingNumberTextBox.Clear();
            RingTypeTextBox.Clear();
            SporePrintColorTextBox.Clear();
            PopulationTextBox.Clear();
            HabitatTextBox.Clear();
            PoisonousTextBox.Clear();
            GillAttachmentTextBox.Clear();
            GillSpacingTextBox.Clear();
            GillSizeTextBox.Clear();
            GillColorTextBox.Clear();
            StalkShapeTextBox.Clear();
            StalkRootTextBox.Clear();
            StalkSurfaceAboveRingTextBox.Clear();
            StalkSurfaceBelowRingTextBox.Clear();
            StalkColorAboveRingTextBox.Clear();
            StalkColorBelowRingTextBox.Clear();

            LoadData();
        }
        private void MushroomDataGrid_CellEditEnding(object sender, SelectedCellsChangedEventArgs e)
        {
            try {

                var editedItem = (Mushroom)e.AddedCells.FirstOrDefault().Item;
                var mushroom = _context.Mushrooms
                    .FirstOrDefault(m => m.Id == editedItem.Id);

                if (mushroom != null)
                {
                    Debug.WriteLine($"Editing Mushroom Id: {editedItem.Id}");

                    mushroom.CapShape = editedItem.CapShape;
                    mushroom.CapSurface = editedItem.CapSurface;
                    mushroom.CapColor = editedItem.CapColor;
                    mushroom.Bruises = editedItem.Bruises;
                    mushroom.Odor = editedItem.Odor;
                    mushroom.VeilType = editedItem.VeilType;
                    mushroom.VeilColor = editedItem.VeilColor;
                    mushroom.RingNumber = editedItem.RingNumber;
                    mushroom.RingType = editedItem.RingType;
                    mushroom.SporePrintColor = editedItem.SporePrintColor;
                    mushroom.Population = editedItem.Population;
                    mushroom.Habitat = editedItem.Habitat;
                    mushroom.Poisonous = editedItem.Poisonous;

                    _context.Update(mushroom);
                    _context.SaveChanges();
                }
            }
            catch
            {
                Debug.WriteLine("Error in editing");
            }
        }

        private void GillDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {

                var editedItem = (Gill)e.AddedCells.FirstOrDefault().Item;
                var gill = _context.Gills
                    .FirstOrDefault(g => g.Id == editedItem.Id);

                if (gill != null)
                {
                    Debug.WriteLine($"Editing Gill Id: {editedItem.Id}");

                    gill.GillAttachment = editedItem.GillAttachment;
                    gill.GillColor = editedItem.GillColor;
                    gill.GillSize = editedItem.GillSize;
                    gill.GillSpacing = editedItem.GillSpacing;

                    _context.Update(gill);
                    _context.SaveChanges();
                }
            }
            catch
            {
                Debug.WriteLine("Error in editing");
            }
        }
        
        private void StalkDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {

                var editedItem = (Stalk)e.AddedCells.FirstOrDefault().Item;
                var stalk = _context.Stalks
                    .FirstOrDefault(s => s.Id == editedItem.Id);

                if (stalk != null)
                {
                    Debug.WriteLine($"Editing Stalk Id: {editedItem.Id}");

                    stalk.StalkRoot = editedItem.StalkRoot;
                    stalk.StalkShape = editedItem.StalkShape;
                    stalk.StalkSurfaceBelowRing = editedItem.StalkSurfaceBelowRing;
                    stalk.StalkColorBelowRing = editedItem.StalkColorBelowRing;
                    stalk.StalkColorAboveRing = editedItem.StalkColorAboveRing;
                    stalk.StalkSurfaceAboveRing = editedItem.StalkSurfaceAboveRing;

                    _context.Update(stalk);
                    _context.SaveChanges();
                }
            }
            catch
            {
                Debug.WriteLine("Error in editing");
            }
        }
    }
}
