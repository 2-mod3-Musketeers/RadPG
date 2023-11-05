using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;
using MyGameLibrary.Properties;
using MyGameLibrary;
using System.Drawing;

namespace Fall2020_CSC403_Project.code
{
    public class Terrain
    {
        public List<Tile> Tiles { get; set; }

        public static Size TileSize = new Size(Screen.PrimaryScreen.Bounds.Width / 35, Screen.PrimaryScreen.Bounds.Width / 35);
        public static int GridWidth = 2 * Screen.PrimaryScreen.Bounds.Width / TileSize.Width;
        public static int GridHeight = 2 * Screen.PrimaryScreen.Bounds.Height / TileSize.Width;


        public Terrain (int Seed, double SeedAmplification)
        {
            this.Tiles = new List<Tile>();
            GenerateTerrain(Seed, SeedAmplification);
        }



        public void AddTile(Tile tile)
        {
            this.Tiles.Add(tile);
        }

        private static Image water_grass = Resources.tile_water_grassy;
        private static Image water_clear = Resources.tile_water_clear;
        private static Image sand = Resources.tile_sand;
        private static Image dirt_path = Resources.tile_dirt_path;
        private static Image grass_light = Resources.tile_grass_light;
        private static Image grass_dark = Resources.tile_grass_dark;
        private static Image cobble_moss = Resources.tile_cobblestone_mossy;
        private static Image cobblestone = Resources.tile_cobblestone;
        private static Image stone_bricks = Resources.tile_stonebrick;
        private static Image bricks = Resources.tile_bricks;

        public void GenerateTerrain(int seed, double amplification = 0)
        {

            Random random = new Random(seed);
            double[][] tiles = new double[GridHeight][];
            for (int i = 0; i < GridHeight; i++)
            {
                tiles[i] = new double[GridWidth];
            }

            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    double noise = random.NextDouble();
                    tiles[y][x] = noise + amplification;

                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < GridHeight; y++) {
                    for (int x = 0; x < GridWidth; x++)
                    {
                        double sum = tiles[y][x];
                        int count = 1;

                        for (int dy = -1; dy <= 1; dy++)
                        {
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                int adjacent_x = x + dx;
                                int adjacent_y = y + dy;

                                if (adjacent_x >= 0 && adjacent_x < GridWidth && adjacent_y >= 0 && adjacent_y < GridHeight && (dx != 0 || dy != 0))
                                {
                                    sum += tiles[adjacent_y][adjacent_x];
                                    count++;
                                }
                            }
                        }

                        tiles[y][x] = sum / count;

                    }
                }
            }

            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    if (tiles[y][x] < 0.4)
                    {
                        this.AddTile(new Tile(water_grass, new Point(x * TileSize.Width, y * TileSize.Width), Tile.EffectType.SuperSlowness));
                    }
                    else if (tiles[y][x] < 0.45)
                    {
                        this.AddTile(new Tile(water_clear, new Point(x * TileSize.Width, y * TileSize.Width), Tile.EffectType.SuperSlowness));
                    }
                    else if (tiles[y][x] < 0.47)
                    {
                        this.AddTile(new Tile(sand, new Point(x * TileSize.Width, y * TileSize.Width), Tile.EffectType.Slowness));
                    }
                    else if (tiles[y][x] < 0.5)
                    {
                        this.AddTile(new Tile(dirt_path, new Point(x * TileSize.Width, y * TileSize.Width)));
                    }
                    else if (tiles[y][x] < 0.55)
                    {
                        this.AddTile(new Tile(grass_light, new Point(x * TileSize.Width, y * TileSize.Width)));
                    }
                    else if (tiles[y][x] < 0.65)
                    {
                        this.AddTile(new Tile(grass_dark, new Point(x * TileSize.Width, y * TileSize.Width)));
                    }
                    else if (tiles[y][x] < 0.7)
                    {
                        this.AddTile(new Tile(cobble_moss, new Point(x * TileSize.Width, y * TileSize.Width)));
                    }
                    else if (tiles[y][x] < 0.72)
                    {
                        this.AddTile(new Tile(cobblestone, new Point(x * TileSize.Width, y * TileSize.Width)));
                    }
                    else if (tiles[y][x] < 75)
                    {
                        this.AddTile(new Tile(stone_bricks, new Point(x * TileSize.Width, y * TileSize.Width), Tile.EffectType.Speed));
                    }
                    else
                    {
                        this.AddTile(new Tile(bricks, new Point(x * TileSize.Width, y * TileSize.Width), Tile.EffectType.Speed));
                    }
                }
            }
        }


    }

}
