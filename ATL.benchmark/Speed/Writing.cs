﻿using System.IO;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System;

namespace ATL.benchmark
{
    public class Writing
    {
        //static string LOCATION = TestUtils.GetResourceLocationRoot()+"MP3/01 - Title Screen_pic.mp3";
        [Params(@"FLAC/flac.flac")]
        public string initialFileLocation;

        private IList<string> tempFiles = new List<string>();


        [GlobalSetup]
        public void Setup(string fileName = "")
        {
            tempFiles.Clear();
            // Duplicate resource
            tempFiles.Add(TestUtils.GenerateTempTestFile(fileName.Length > 0 ? fileName : initialFileLocation));
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            // Mass delete resulting files
            foreach (string s in tempFiles)
            {
                File.Delete(s);
            }

            tempFiles.Clear();
        }

        [Benchmark(Baseline = true)]
        public void Perf_Write()
        {
            performWrite();
        }

        private void displayProgress(float progress)
        {
            Console.WriteLine(progress * 100 + "%");
        }

        private void performWrite()
        {
            // Mass-read resulting files
            foreach (string s in tempFiles) performWrite(s);
        }

        public void performWrite(String filePath)
        {
            IProgress<float> progress = new Progress<float>(displayProgress);
            Track t = new Track(filePath/*, progress*/);

            //t.AdditionalFields.Add(new KeyValuePair<string, string>("test", "aaa"));
            // Modify metadata
            t.Artist = "Hey ho";
            t.Composer = "Oscar Wilde";
            t.Album = "Fake album starts here and is longer than the original one";

            if (t.EmbeddedPictures.Count > 0) t.EmbeddedPictures.Clear();
            t.EmbeddedPictures.Add(PictureInfo.fromBinaryData(File.ReadAllBytes(@"E:\temp\mp3\windowsIcon\folder.jpg")));

            t.Save();
        }
    }
}
