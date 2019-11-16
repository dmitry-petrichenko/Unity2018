# -----------------------
# Remove unnecesary nuget libs
# -----------------------
import os
import shutil

dirname = os.path.dirname(__file__)
base_dir_path = os.path.join(dirname, 'Assets\Plugins')

def removeAllDirsInListExceptOne(directories_list, the_one):
    for dir in directories_list:
        if os.path.basename(dir) != os.path.basename(the_one):
            shutil.rmtree(dir)

def tryGetDirrectoryInList(list, dirrectory_name):
    for dir in list:
        if dirrectory_name in os.path.basename(dir):
            return dir
    return None

def getImmediateSubdirectoriesIn(dir):
    sub_dirs = next(os.walk(dir))[1]
    new_dirs = []
    for x in sub_dirs:
        new_dirs.append(os.path.join(dir, x))
    return new_dirs

def removeNotUsedDirectoriesIn(directory):
    sub_dirs = getImmediateSubdirectoriesIn(directory)
    one = tryGetDirrectoryInList(sub_dirs, "netstandard2.0")
    if one is not None:
        removeAllDirsInListExceptOne(sub_dirs, one)
        return

    one = tryGetDirrectoryInList(sub_dirs, "netstandard")
    if one is not None:
        removeAllDirsInListExceptOne(sub_dirs, one)

# start -----------------------

all_lib_dirrectories = []

for root, dirs, files in os.walk(base_dir_path):
    for dir in dirs:
        if dir == "lib":
            all_lib_dirrectories.append(os.path.join(root, dir))

for libDir in all_lib_dirrectories:
    removeNotUsedDirectoriesIn(libDir)

for root, dirs, files in os.walk(base_dir_path):
    for file in files:
        if file.endswith('.meta'):
            os.remove(os.path.join(root, file))
