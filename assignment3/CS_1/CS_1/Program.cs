using System;

namespace CS_1
{
    abstract class Geometry
    {
        public abstract bool IsValid();
        public abstract void CalArea();
    }

    class Rect : Geometry
    {
        private double _length;
        private double _width;
        private double _area;
        public double Length
        {
            get => _length;
            set => _length = value;
        }
        public double Width
        {
            get => _width;
            set => _width = value;
        }
        public double Area
        {
            get => _area;
            set => _area = value;
        }

        public override bool IsValid()
        {
            return Length > 0 && Width > 0;
        }
        public override void CalArea()
        {
            if (IsValid())
            {
                Area = Length * Width;
            }
            else
            {
                throw new ArgumentException("The geometry is invalid!");
            }
        }
    }

    class Square : Geometry
    {
        private double _side;
        private double _area;
        public double Side
        {
            get => _side;
            set => _side = value;
        }
        public double Area
        {
            get => _area;
            set => _area = value;
        }

        public override bool IsValid()
        {
            return Side > 0;
        }

        public override void CalArea()
        {
            if (IsValid())
            {
                Area = Side * Side;
            }
            else
            {
                throw new ArgumentException("The geometry is invalid!");
            }
        }
    }

    class Trian : Geometry
    {
        private double _line1;
        private double _line2;
        private double _line3;
        private double _area;
        public double Line1
        {
            get => _line1;
            set => _line1 = value;
        }
        public double Line2
        {
            get => _line2;
            set => _line2 = value;
        }
        public double Line3
        {
            get => _line3;
            set => _line3 = value;
        }
        public double Area
        {
            get => _area;
            set => _area = value;
        }

        public override bool IsValid()
        {
            return (Line1 + Line2) > Line3 && (Line2 + Line3) > Line1 && (Line3 + Line1) > Line2;
        }
        public override void CalArea()
        {
            if (IsValid())
            {
                double p = (Line1 + Line2 + Line3) / 2;
                Area = Math.Sqrt(p * (p - Line1) * (p - Line2) * (p - Line3));
            }
            else
            {
                throw new ArgumentException("The geometry is invalid!");
            }
        }
    }

    class SimpleFactory
    {
        public static Geometry CreateGeo(string geoName)
        {
            Geometry newGeo = null;
            if (geoName == "Rect")
            {
                newGeo = new Rect();
            }
            else if (geoName == "Square")
            {
                newGeo = new Square();
            }
            else if (geoName == "Trian")
            {
                newGeo = new Trian();
            }
            else
            {
                throw new ArgumentException("Wrong geometry name!");
            }
            return newGeo;
        }
    }

    class Client
    {
        public static double GetRandomNumber(double minimum, double maximum, int Len)   //Len小数点保留位数
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * (maximum - minimum) + minimum, Len);
        }

        static void Main(string[] args)
        {
            
            double totalArea = 0;
            
            for (int i = 0; i < 10; i++)
            {
                
                Random random=new Random();
                int shapeType=random.Next(0,2);

                
                if (shapeType == 0) // Rectangle
                {
                    Rect rect = (Rect)SimpleFactory.CreateGeo("Rect");
                    rect.Length = GetRandomNumber(1, 10, 2);
                    rect.Width = GetRandomNumber(1, 10, 2);
                    rect.CalArea();

                    totalArea += rect.Area;
                }
                else if (shapeType == 1) // Square
                {
                    Square square = (Square)SimpleFactory.CreateGeo("Square");
                    square.Side = GetRandomNumber(1, 10, 2);
                    square.CalArea();
                    totalArea += square.Area;
                }
                else // Triangle
                {
                    Trian trian = (Trian)SimpleFactory.CreateGeo("Trian");
                    trian.Line1 = GetRandomNumber(1, 10, 2);
                    trian.Line2 = GetRandomNumber(1, 10, 2);
                    trian.Line3 = GetRandomNumber(1, 10, 2);
                    trian.CalArea();
                    totalArea += trian.Area;
                }

                
            }

            Console.WriteLine($"Total area of all shapes: {totalArea}");
        }
    }
}
