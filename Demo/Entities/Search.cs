using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Demo.Entities
{
    public class SearchConditions
    {
        public List<string> area = new List<string>();
        public List<string> datasource = new List<string>() ;
        public List<long> scale = new List<long>();
        public List<float> resolution = new List<float>();
        public string keywords;

    }

    public class Search
    {
        public int IsIntersect(int maxx, int maxy, int minx, int miny)
        {

            int minx1 = 10, miny1 = 10, maxx1 = 20, maxy1 = 20;

            if (maxx < maxx1 && minx > minx1 && maxy < maxy1 && miny > miny1)
                return 1;
            if (maxx > maxx1 && minx < minx1 && maxy > maxy1 && miny < miny1)
                return 2;
            int mmaxx = maxx >= maxx1 ? maxx : maxx1;
            int mminx = minx <= minx1 ? minx : minx1;
            int mmaxy = maxy >= maxy1 ? maxy : maxy1;
            int mminy = miny <= miny1 ? miny : miny1;
            if (!(mminx > mmaxx || mminy > mmaxy))
                return 3;

            return 0;
        }

    }
}
