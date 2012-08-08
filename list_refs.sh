for f in bin/mono/*.dll; do 
    filename=$(basename "$f")
    version=`~/getver "$f"`
    refs=`~/mrefs "$f"`
    echo -e "" 
    echo -e "$filename:\t$version" 
    echo -e "=================" 
    echo -e "$refs" 
done
