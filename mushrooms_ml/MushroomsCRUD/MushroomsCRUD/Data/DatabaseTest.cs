using System.Diagnostics;

namespace MushroomsCRUD.Data
{
    public class DatabaseTest
    {
        public static void TestConnection()
        {
            using (var context = new AppDbContext())
            {
                var mushrooms = context.Mushrooms.ToList();

                foreach (var mushroom in mushrooms)
                {
                    //Debug.WriteLine($"Mushroom ID: {mushroom.Id}, Cap Shape: {mushroom.CapShape}");
                }
            }
        }
    }
}
