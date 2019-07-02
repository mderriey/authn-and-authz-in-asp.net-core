git reset --hard HEAD
git checkout $(git rev-list --topo-order HEAD..master | Select-Object -Last 1)