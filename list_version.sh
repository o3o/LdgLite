for f in bin/mono/*.dll; do 
    filename=$(basename "$f")
    version=`~/getver "$f"`
    echo -e "$filename:\t$version" 
done
