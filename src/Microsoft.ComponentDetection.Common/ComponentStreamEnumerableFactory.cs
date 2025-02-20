﻿using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using Microsoft.ComponentDetection.Contracts;

namespace Microsoft.ComponentDetection.Common
{
    [Export(typeof(IComponentStreamEnumerableFactory))]
    [Shared]
    public class ComponentStreamEnumerableFactory : IComponentStreamEnumerableFactory
    {
        [Import]
        public ILogger Logger { get; set; }

        [Import]
        public IPathUtilityService PathUtilityService { get; set; }

        public IEnumerable<IComponentStream> GetComponentStreams(DirectoryInfo directory, IEnumerable<string> searchPatterns, ExcludeDirectoryPredicate directoryExclusionPredicate, bool recursivelyScanDirectories = true)
        {
            SafeFileEnumerable enumerable = new SafeFileEnumerable(directory, searchPatterns, Logger, PathUtilityService, directoryExclusionPredicate, recursivelyScanDirectories);
            return new ComponentStreamEnumerable(enumerable, Logger);
        }

        public IEnumerable<IComponentStream> GetComponentStreams(DirectoryInfo directory, Func<FileInfo, bool> fileMatchingPredicate, ExcludeDirectoryPredicate directoryExclusionPredicate, bool recursivelyScanDirectories = true)
        {
            SafeFileEnumerable enumerable = new SafeFileEnumerable(directory, fileMatchingPredicate, Logger, PathUtilityService, directoryExclusionPredicate, recursivelyScanDirectories);
            return new ComponentStreamEnumerable(enumerable, Logger);
        }
    }
}
