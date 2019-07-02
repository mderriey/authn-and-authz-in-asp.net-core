$log = git log --oneline
$message = 'Add services and middleware'
$result = $log | Where-Object { $_ -like "*$message*" }
$commit = $result -split ' ' | Select-Object -First 1
git reset --hard HEAD
git co $commit