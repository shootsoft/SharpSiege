A http request testing tool like siege written in c#

Single thread, single url, retry N.

Useage:

sharpsiege [-r 10] {-u url}

-r Retry times, Default 10

-u Tested Url

Example:

sharpsiege -t 10 -u "http://www.baidu.com"

todo:
Mutiple clients

Mutiple URLs and random hits

File input URLs

TIMED testing

Request timeout

Request length

Custom headers

Custom user-agent