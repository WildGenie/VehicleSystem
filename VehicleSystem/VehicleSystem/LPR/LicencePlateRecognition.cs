using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using com.sun.org.apache.bcel.@internal.generic;
using com.sun.org.apache.xml.@internal.utils.res;
using SimpleLPR2;
using VehicleSystem.Interfaces;

namespace VehicleSystem.LPR
{
    public class LicencePlateRecognition
    {
        public enum QUALITY
        {
            LOW,
            MEDIUM,
            HIGH
        };

        public QUALITY DETECTIONQUALITY = QUALITY.HIGH;
        private double quality;

        //LicencePlateRecognizer & Processer
        ISimpleLPR _lpr;
        IProcessor _proc;

        public event CustomizedEvents.finishedProcessingPlate processingPlateFinished;

        public LicencePlateRecognition()
        {
            switch (DETECTIONQUALITY)
            {
                case QUALITY.LOW:
                {
                    quality = 0.5;
                    break;  
                }
                case QUALITY.MEDIUM:
                {
                    quality = 0.75;
                    break;
                }
                default:
                {
                    quality = 0.9;
                    break;
                }
            }

            string sCountry = "South Africa";

            _lpr = SimpleLPR.Setup();
            //use product key
            //_lpr.set_productKey("key_nmmu.xml");
            _lpr.set_countryWeight(sCountry, 1.0f);
            _lpr.realizeCountryWeights();

            _proc = _lpr.createProcessor();
        }

        String _lastDetectedPlate = "";
        public void analyze(Bitmap image)
        {
            Thread thread = new Thread(() =>
            {
                List<Candidate> results = new List<Candidate>();
                try
                {
                    //TODO
                    //figure out why we sometimes have a memory access violation.
                    //could be that the LPR was called by 2 threats at the exact same time.
                    //maybe c# is trying to give the LPR the same memory addresses since it is running out of RAM?
                    //this never happens with a few streams, and only seldom happens with many streams, I don't see this 
                    //changing any results, as license plate will be recognised either before, or after this expection is caught
                    //added this to app.config, to force .NET 3.5 corruption state handling
                    // <runtime>
                    //  <legacyCorruptedStateExceptionsPolicy enabled="true"/>
                    // </runtime>
                    results = _proc.analyze(image, 120);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Boolean alreadyProcessed = false;
                foreach (var item in results)
                {
                    //Console.WriteLine(item.text + "  | " + item.confidence);
                    if (_lastDetectedPlate.Replace(" ","").Contains(item.text.Replace(" ", "")))
                        alreadyProcessed = true;
                }

                //want only 'good' results
                if (results.Count > 0)
                    if (results[0].confidence < quality)
                        alreadyProcessed = true;

                if (processingPlateFinished != null && results.Count > 0 && !alreadyProcessed)
                {
                    //find best cofidence rating for lastDetectedPlate
                    int pos = 0;
                    double max = results[0].confidence;
                    for (int k = 1; k < results.Count; k++)
                        if (results[0].confidence > max)
                        {
                            pos = k;
                            max = results[0].confidence;
                        }

                    _lastDetectedPlate = results[pos].text;

                    if (processingPlateFinished != null)
                        processingPlateFinished(this, results[pos], image);

                    Console.WriteLine(results[pos].text + " | " + results[pos].confidence);
                }
            });
            thread.Start();
        }
    }
}
