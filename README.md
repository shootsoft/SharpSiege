A http request testing tool like siege written in c#

Useage:

sharpsiege [-r 10] {-u url}

-r Retry times, Default 10
-u Tested Url

Example:

sharpsiege -t 10 -u "http://www.baidu.com"