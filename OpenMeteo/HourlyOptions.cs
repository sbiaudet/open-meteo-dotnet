﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace OpenMeteo
{
    public class HourlyOptions : IEnumerable, ICollection<string>
    {
        public static HourlyOptions All { get { return new HourlyOptions(_allHourlyParams); } }

        /// <summary>
        /// A copy of the current applied parameter. This is a COPY. Editing anything inside this copy won't be applied 
        /// </summary>
        public List<string> Parameter { get { return new List<string>(_parameter); } }

        public int Count => _parameter.Count;

        public bool IsReadOnly => false;

        private static readonly string[] _allHourlyParams = new string[]
        {
            "temperature_2m",
            "relativehumidity_2m",
            "dewpoint_2m",
            "apparent_temperature",
            "pressure_msl",
            "cloudcover",
            "cloudcover_low",
            "cloudcover_mid",
            "cloudcover_high",
            "windspeed_10m",
            "windspeed_80m",
            "windspeed_120m",
            "windspeed_180m",
            "winddirection_10m",
            "winddirection_80m",
            "winddirection_120m",
            "winddirection_180m",
            "windgusts_10m",
            "shortwave_radiation",
            "direct_radiation",
            "diffuse_radiation",
            "vapor_pressure_deficit",
            "evapotranspiration",
            "precipitation",
            "weathercode",
            "snow_height",
            "freezinglevel_height",
            "soil_temperature_0cm",
            "soil_temperature_6cm",
            "soil_temperature_18cm",
            "soil_temperature_54cm",
            "soil_moisture_0_1cm",
            "soil_moisture_1_3cm",
            "soil_moisture_3_9cm",
            "soil_moisture_9_27cm",
            "soil_moisture_27_81cm"
        };

        private readonly List<string> _parameter = new List<string>();
        public HourlyOptions(string[] parameter)
        {
            foreach (string s in parameter)
            {
                if (!IsValidParameter(s.ToLower()))
                    throw new ArgumentException();
                this._parameter.Add(s);
            }
        }

        public HourlyOptions(string parameter)
        {
            string s = parameter.ToLower();
            if (!IsValidParameter(s))
                throw new ArgumentException();
            this._parameter.Add(s);
        }

        public HourlyOptions(HourlyOptionsParameter parameter)
        {
            bool result = Add(parameter);
            if (!result)
                throw new ArgumentException();
        }

        public HourlyOptions(HourlyOptionsParameter[] parameter)
        {
            bool result = Add(parameter);
            if (!result)
                throw new ArgumentException();
        }

        public HourlyOptions()
        {

        }

        public bool Add(HourlyOptionsParameter param)
        {
            // Each enum variable represents an integer starting with 0.
            // So we can use our static string[] to get the string representation

            // Make sure we aren't our of array
            if ((int)param < 0 || (int)param >= _allHourlyParams.Length) return false;

            string paramToAdd = _allHourlyParams[(int)param];

            // Check that the parameter isn't already added
            if (this._parameter.Contains(paramToAdd)) return false;

            _parameter.Add(paramToAdd);
            return true;
        }

        public bool Add(HourlyOptionsParameter[] param)
        {
            foreach (HourlyOptionsParameter paramToAdd in param)
            {
                if (!Add(paramToAdd))
                    return false;
            }
            return true;
        }

        private bool IsValidParameter(string s)
        {
            bool found = false;
            foreach (string str in _allHourlyParams)
            {
                if (found) break;
                if (s == str)
                    found = true;
            }
            return found;
        }

        public IEnumerator GetEnumerator()
        {
            return _parameter.GetEnumerator();
        }

        public void Add(string item)
        {
            // Make sure that item is a valid parameter
            if (!IsValidParameter(item)) return;

            _parameter.Add(item);
        }

        public void Clear()
        {
            _parameter.Clear();
        }

        public bool Contains(string item)
        {
            // Don't look through object is input is invaid
            if (!IsValidParameter(item)) return false;
            return _parameter.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            _parameter.CopyTo(array, arrayIndex);
        }

        public bool Remove(string item)
        {
            if (!IsValidParameter(item)) return false;
            return _parameter.Remove(item);
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _parameter.GetEnumerator();
        }
    }

    public enum HourlyOptionsParameter
    {
        temperature_2m,
        relativehumidity_2m,
        dewpoint_2m,
        apparent_temperature,
        pressure_msl,
        cloudcover,
        cloudcover_low,
        cloudcover_mid,
        cloudcover_high,
        windspeed_10m,
        windspeed_80m,
        windspeed_120m,
        windspeed_180m,
        winddirection_10m,
        winddirection_80m,
        winddirection_120m,
        winddirection_180m,
        windgusts_10m,
        shortwave_radiation,
        direct_radiation,
        diffuse_radiation,
        vapor_pressure_deficit,
        evapotranspiration,
        precipitation,
        weathercode,
        snow_height,
        freezinglevel_height,
        soil_temperature_0cm,
        soil_temperature_6cm,
        soil_temperature_18cm,
        soil_temperature_54cm,
        soil_moisture_0_1cm,
        soil_moisture_1_3cm,
        soil_moisture_3_9cm,
        soil_moisture_9_27cm,
        soil_moisture_27_81cm
    }
}