// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013 lambdalice
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Place : CoreBase
    {
        
        /// <summary>
        /// Contains a hash of variant information about the place. 
        /// </summary>
        /// <see cref="https://dev.twitter.com/docs/about-geo-place-attributes"/>
        public string Attributes { get; set; }
        
        /// <summary>
        /// A bounding box of coordinates which encloses this place.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }
        
        public Place[] ContainedWithin { get; set; }
        
        /// <summary>
        /// Name of the country containing this place.The country.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Shortened country code representing the country containing this place.
        /// </summary>
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Full human-readable representation of the place's name.
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// ID representing this place. Note that this is represented as a string, not an integer.
        /// In trends/avaliable or trends/closest, ID is a Yahoo! Where On Earth ID.
        /// </summary>
        public string Id { get; set; }
        
        public string ParentId{ get; set; }
        
        /// <summary>
        /// Short human-readable representation of the place's name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The type of location represented by this place.
        /// </summary>
        public string PlaceType { get; set; }
        
        public int? PlaceTypeCode{ get; set; }
        
        public string[] Polylines{ get; set; }
        
        /// <summary>
        /// URL representing the location of additional place metadata for this place.
        /// </summary>
        public Uri Url { get; set; }
        
        public Place(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Attributes = e.IsDefined("attributes") ? e.attributes.ToString() : null;
            BoundingBox = e.IsDefined("bounding_box") ? CoreBase.Convert<BoundingBox>(this.Tokens, e.bounding_box) : null;
            ContainedWithin = e.IsDefined("contained_within") ? CoreBase.ConvertArray<Place>(this.Tokens, e.contained_within) : null;
            Country = e.country;
            CountryCode = e.IsDefined("countryCode") ? e.countryCode : e.country_code;
            FullName = e.IsDefined("full_name") ? e.full_name : null;
            Id = e.IsDefined("woeid") ? e.woeid.ToString() : e.id;
            ParentId = e.IsDefined("parentid") ? e.parentid.ToString() : null;
            Name = e.name;
            PlaceType = e.IsDefines("placeType") ? e.placeType.name : e.place_type;
            PlaceTypeCode = e.IsDefines("placeType") ? e.placeType.code : null;
            Polylines = e.IsDefined("polylines") ? (e.polylines as dynamic[]).Cast<string>().ToArray() : null;
            Url = new Uri(e.url);
        }
    }

    public class BoundingBox : CoreBase
    {
        /// <summary>
        /// A series of longitude and latitude points, defining a box which will contain the Place entity this bounding box is related to. Each point is an array in the form of [longitude, latitude]. Points are grouped into an array per bounding box. Bounding box arrays are wrapped in one additional array to be compatible with the polygon notation.
        /// </summary>
        public double[][][] Coordinates { get; set; }
        
        /// <summary>
        /// The type of data encoded in the coordinates property. This will be "Polygon" for bounding boxes.
        /// </summary>
        public string Type { get; set; }
        
        public BoundingBox(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Coordinates = (e.coordinates as dynamic[][][]).Select(
                x => x.Select(
                y => y.Select(
                z => ((double)z)).ToArray())
                .ToArray() as double[][])
                .ToArray() as double[][][];
            Type = e.type;
        }
        
    }
    
    public class GeoResult : CoreBase, IEnumerable<Place>
    {
        /// <summary>
        /// Places.
        /// </summary>
        public Place[] Places { get; set; }

        /// <summary>
        /// The token needed to be able to create a new place.
        /// </summary>
        public string Token { get; set; }
        
        public GeoResult(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Places = CoreBase.ConvertArray<Place>(this.Tokens, e.places);
            Token = e.IsDefined("token") ? e.token : null;
        }
        
        public System.Collections.IEnumerator GetEnumerator()
        {
            return Places.GetEnumerator();
        }
        
        IEnumerator<Place> IEnumerable<Place>.GetEnumerator()
        {
            return (Places as IEnumerable<Place>).GetEnumerator();
        }
    }
    
    public class TrendsResult : CoreBase, IEnumerable<SearchQuery>
    {
        public DateTimeOffset AsOf{ get; set; }

        public DateTimeOffset CreatedAt{ get; set; }
        
        public string[] Locations{ get; set; }
        
        public long[] LocationIds{ get; set; }
        
        public SearchQuery[] Trends{ get; set; }
        
        public TrendsResult(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            AsOf = DateTimeOffset.ParseExact(e.as_of, "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo, 
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);

            CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo, 
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            Locations = (e.locations as dynamic[]).Select(x => (string)x.name).ToArray();
            LocationIds = (e.locations as dynamic[]).Select(x => (long)x.woeid).ToArray();
            Trends = CoreBase.ConvertArray<SearchQuery>(this.Tokens, e.trends);
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Trends.GetEnumerator();
        }
        
        public IEnumerator<SearchQuery> GetEnumerator()
        {
            return (Trends as IEnumerable<SearchQuery>).GetEnumerator();
        } 
    }
}