#!/usr/bin/python

# Copyright 2015 Urban Airship and Contributors

from __future__ import print_function
import sys
from mod_pbxproj import XcodeProject

def main(argv):
    install_path = argv[1]
    project_name = 'Unity-iPhone'

    # Add Frameworks
    frameworks = ['usr/lib/libz.dylib'
            , 'usr/lib/libsqlite3.dylib'
            , 'System/Library/Frameworks/CoreTelephony.framework'
            , 'System/Library/Frameworks/CoreData.framework'
            , 'System/Library/Frameworks/Security.framework'
            , 'System/Library/Frameworks/CoreData.framework'
            , 'System/Library/Frameworks/CoreLocation.framework'
            , 'System/Library/Frameworks/PassKit.framework']

    compiler_flags = ['-ObjC']
    
    project = XcodeProject.Load(install_path+'/'+project_name+'.xcodeproj/project.pbxproj')
    library_group = project.get_or_create_group('Libraries')
    project.add_file_if_doesnt_exist('../Assets/Plugins/iOS/AirshipConfig.plist', parent=library_group)
    project.add_header_search_paths('../Assets/Plugins/iOS/Airship')

    for compiler_flag in compiler_flags:
        project.add_other_ldflags(compiler_flag)
    
    for framework in frameworks:
        project.add_file(framework, tree='SDKROOT')

    if project.modified:
        project.backup()
        project.save()


if __name__ == "__main__":
    main(sys.argv)
