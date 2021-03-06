﻿
import * as React from 'react'
import { Tab, Tabs } from 'react-bootstrap'
import { classes } from '../../../../Framework/Signum.React/Scripts/Globals'
import { FormGroup, FormControlStatic, ValueLine, ValueLineType, EntityLine, EntityCombo, EntityDetail, EntityList, EntityRepeater, EntityTabRepeater } from '../../../../Framework/Signum.React/Scripts/Lines'
import { SubTokensOptions, QueryToken, QueryTokenType, hasAnyOrAll } from '../../../../Framework/Signum.React/Scripts/FindOptions'
import Typeahead  from '../../../../Framework/Signum.React/Scripts/Lines/Typeahead'
import { SearchControl } from '../../../../Framework/Signum.React/Scripts/Search'
import { getToString, getMixin } from '../../../../Framework/Signum.React/Scripts/Signum.Entities'
import { TypeContext, FormGroupStyle } from '../../../../Framework/Signum.React/Scripts/TypeContext'


export interface IconTypeaheadLineProps {
    ctx: TypeContext<string | null | undefined>;
    onChange?: () => void;
    extraIcons?: string[];

}

export class IconTypeaheadLine extends React.Component<IconTypeaheadLineProps, void>{

    render() {
        var ctx = this.props.ctx;

        return (
            <FormGroup ctx={ctx} labelText={ctx.niceName()} >
                <IconTypeahead icon={ctx.value}
                    extraIcons={this.props.extraIcons}
                    onChange={newIcon => {
                        ctx.value = newIcon;
                        if (this.props.onChange)
                            this.props.onChange();
                        this.forceUpdate();
                    } }/>
            </FormGroup>
        );
    }
}

export interface IconTypeaheadProps {
    icon: string | null | undefined;
    onChange: (newIcon: string | null | undefined) => void;
    extraIcons?: string[];
}

export class IconTypeahead extends React.Component<IconTypeaheadProps, void>{

    icons: string[]; 
    constructor(props: IconTypeaheadProps) {
        super(props);

        this.icons = ([] as string[]).concat(props.extraIcons || []).concat(fontAwesome).concat(glyphicon);
    }

    handleGetItems = (query: string) => {
        if (!query)
            return Promise.resolve(([] as string[]).concat(this.props.extraIcons || []).concat(["fa fa-", "glyphicon glyphicon-"]));

        const result = this.icons
            .filter(k => k.toLowerCase().contains(query.toLowerCase()))
            .orderBy(a => a.trimStart("fa fa-").trimStart("glyphicon glyphicon-").length)
            .filter((k, i) => i < 5);

        return Promise.resolve(result);
    }

    handleSelect = (item: string) => {
        this.props.onChange(item);
        this.forceUpdate();
        return item;
    }

    handleRenderItem = (item: string, query: string) => {

        return (
            <span>
                <span className={classes("icon", item)} style={{ width: "12px", height: "12px" }} />
                {Typeahead.highlightedText(item, query)}
            </span>
        );
    }

    render() {
        return (
            <div style={{ position: "relative" }}>
                <Typeahead
                    value={this.props.icon || ""}
                    inputAttrs={{ className: "form-control sf-entity-autocomplete" }}
                    getItems={this.handleGetItems}
                    onSelect={this.handleSelect}
                    onChange={this.handleSelect}
                    renderItem={this.handleRenderItem}
                    minLength={0}
                    />
            </div>
        );
    }
}


const fontAwesome = `address-book
address-book-o
address-card
address-card-o
bandcamp
bath
bathtub
drivers-license
drivers-license-o
eercast
envelope-open
envelope-open-o
etsy
free-code-camp
grav
handshake-o
id-badge
id-card
id-card-o
imdb
linode
meetup
microchip
podcast
quora
ravelry
s15
shower
snowflake-o
superpowers
telegram
thermometer
thermometer-0
thermometer-1
thermometer-2
thermometer-3
thermometer-4
thermometer-empty
thermometer-full
thermometer-half
thermometer-quarter
thermometer-three-quarters
times-rectangle
times-rectangle-o
user-circle
user-circle-o
user-o
vcard
vcard-o
window-close
window-close-o
window-maximize
window-minimize
window-restore
wpexplorer
address-book
address-book-o
address-card
address-card-o
adjust
american-sign-language-interpreting
anchor
archive
area-chart
arrows
arrows-h
arrows-v
asl-interpreting
assistive-listening-systems
asterisk
at
audio-description
automobile
balance-scale
ban
bank
bar-chart
bar-chart-o
barcode
bars
bath
bathtub
battery
battery-0
battery-1
battery-2
battery-3
battery-4
battery-empty
battery-full
battery-half
battery-quarter
battery-three-quarters
bed
beer
bell
bell-o
bell-slash
bell-slash-o
bicycle
binoculars
birthday-cake
blind
bluetooth
bluetooth-b
bolt
bomb
book
bookmark
bookmark-o
braille
briefcase
bug
building
building-o
bullhorn
bullseye
bus
cab
calculator
calendar
calendar-check-o
calendar-minus-o
calendar-o
calendar-plus-o
calendar-times-o
camera
camera-retro
car
caret-square-o-down
caret-square-o-left
caret-square-o-right
caret-square-o-up
cart-arrow-down
cart-plus
cc
certificate
check
check-circle
check-circle-o
check-square
check-square-o
child
circle
circle-o
circle-o-notch
circle-thin
clock-o
clone
close
cloud
cloud-download
cloud-upload
code
code-fork
coffee
cog
cogs
comment
comment-o
commenting
commenting-o
comments
comments-o
compass
copyright
creative-commons
credit-card
credit-card-alt
crop
crosshairs
cube
cubes
cutlery
dashboard
database
deaf
deafness
desktop
diamond
dot-circle-o
download
drivers-license
drivers-license-o
edit
ellipsis-h
ellipsis-v
envelope
envelope-o
envelope-open
envelope-open-o
envelope-square
eraser
exchange
exclamation
exclamation-circle
exclamation-triangle
external-link
external-link-square
eye
eye-slash
eyedropper
fax
feed
female
fighter-jet
file-archive-o
file-audio-o
file-code-o
file-excel-o
file-image-o
file-movie-o
file-pdf-o
file-photo-o
file-picture-o
file-powerpoint-o
file-sound-o
file-video-o
file-word-o
file-zip-o
film
filter
fire
fire-extinguisher
flag
flag-checkered
flag-o
flash
flask
folder
folder-o
folder-open
folder-open-o
frown-o
futbol-o
gamepad
gavel
gear
gears
gift
glass
globe
graduation-cap
group
hand-grab-o
hand-lizard-o
hand-paper-o
hand-peace-o
hand-pointer-o
hand-rock-o
hand-scissors-o
hand-spock-o
hand-stop-o
handshake-o
hard-of-hearing
hashtag
hdd-o
headphones
heart
heart-o
heartbeat
history
home
hotel
hourglass
hourglass-1
hourglass-2
hourglass-3
hourglass-end
hourglass-half
hourglass-o
hourglass-start
i-cursor
id-badge
id-card
id-card-o
image
inbox
industry
info
info-circle
institution
key
keyboard-o
language
laptop
leaf
legal
lemon-o
level-down
level-up
life-bouy
life-buoy
life-ring
life-saver
lightbulb-o
line-chart
location-arrow
lock
low-vision
magic
magnet
mail-forward
mail-reply
mail-reply-all
male
map
map-marker
map-o
map-pin
map-signs
meh-o
microchip
microphone
microphone-slash
minus
minus-circle
minus-square
minus-square-o
mobile
mobile-phone
money
moon-o
mortar-board
motorcycle
mouse-pointer
music
navicon
newspaper-o
object-group
object-ungroup
paint-brush
paper-plane
paper-plane-o
paw
pencil
pencil-square
pencil-square-o
percent
phone
phone-square
photo
picture-o
pie-chart
plane
plug
plus
plus-circle
plus-square
plus-square-o
podcast
power-off
print
puzzle-piece
qrcode
question
question-circle
question-circle-o
quote-left
quote-right
random
recycle
refresh
registered
remove
reorder
reply
reply-all
retweet
road
rocket
rss
rss-square
s15
search
search-minus
search-plus
send
send-o
server
share
share-alt
share-alt-square
share-square
share-square-o
shield
ship
shopping-bag
shopping-basket
shopping-cart
shower
sign-in
sign-language
sign-out
signal
signing
sitemap
sliders
smile-o
snowflake-o
soccer-ball-o
sort
sort-alpha-asc
sort-alpha-desc
sort-amount-asc
sort-amount-desc
sort-asc
sort-desc
sort-down
sort-numeric-asc
sort-numeric-desc
sort-up
space-shuttle
spinner
spoon
square
square-o
star
star-half
star-half-empty
star-half-full
star-half-o
star-o
sticky-note
sticky-note-o
street-view
suitcase
sun-o
support
tablet
tachometer
tag
tags
tasks
taxi
television
terminal
thermometer
thermometer-0
thermometer-1
thermometer-2
thermometer-3
thermometer-4
thermometer-empty
thermometer-full
thermometer-half
thermometer-quarter
thermometer-three-quarters
thumb-tack
thumbs-down
thumbs-o-down
thumbs-o-up
thumbs-up
ticket
times
times-circle
times-circle-o
times-rectangle
times-rectangle-o
tint
toggle-down
toggle-left
toggle-off
toggle-on
toggle-right
toggle-up
trademark
trash
trash-o
tree
trophy
truck
tty
tv
umbrella
universal-access
university
unlock
unlock-alt
unsorted
upload
user
user-circle
user-circle-o
user-o
user-plus
user-secret
user-times
users
vcard
vcard-o
video-camera
volume-control-phone
volume-down
volume-off
volume-up
warning
wheelchair
wheelchair-alt
wifi
window-close
window-close-o
window-maximize
window-minimize
window-restore
wrench
american-sign-language-interpreting
asl-interpreting
assistive-listening-systems
audio-description
blind
braille
cc
deaf
deafness
hard-of-hearing
low-vision
question-circle-o
sign-language
signing
tty
universal-access
volume-control-phone
wheelchair
wheelchair-alt
hand-grab-o
hand-lizard-o
hand-o-down
hand-o-left
hand-o-right
hand-o-up
hand-paper-o
hand-peace-o
hand-pointer-o
hand-rock-o
hand-scissors-o
hand-spock-o
hand-stop-o
thumbs-down
thumbs-o-down
thumbs-o-up
thumbs-up
ambulance
automobile
bicycle
bus
cab
car
fighter-jet
motorcycle
plane
rocket
ship
space-shuttle
subway
taxi
train
truck
wheelchair
wheelchair-alt
genderless
intersex
mars
mars-double
mars-stroke
mars-stroke-h
mars-stroke-v
mercury
neuter
transgender
transgender-alt
venus
venus-double
venus-mars
file
file-archive-o
file-audio-o
file-code-o
file-excel-o
file-image-o
file-movie-o
file-o
file-pdf-o
file-photo-o
file-picture-o
file-powerpoint-o
file-sound-o
file-text
file-text-o
file-video-o
file-word-o
file-zip-o
info-circle fa-lg fa-li
circle-o-notch
cog
gear
refresh
spinner
check-square
check-square-o
circle
circle-o
dot-circle-o
minus-square
minus-square-o
plus-square
plus-square-o
square
square-o
cc-amex
cc-diners-club
cc-discover
cc-jcb
cc-mastercard
cc-paypal
cc-stripe
cc-visa
credit-card
credit-card-alt
google-wallet
paypal
area-chart
bar-chart
bar-chart-o
line-chart
pie-chart
bitcoin
btc
cny
dollar
eur
euro
gbp
gg
gg-circle
ils
inr
jpy
krw
money
rmb
rouble
rub
ruble
rupee
shekel
sheqel
try
turkish-lira
usd
won
yen
align-center
align-justify
align-left
align-right
bold
chain
chain-broken
clipboard
columns
copy
cut
dedent
eraser
file
file-o
file-text
file-text-o
files-o
floppy-o
font
header
indent
italic
link
list
list-alt
list-ol
list-ul
outdent
paperclip
paragraph
paste
repeat
rotate-left
rotate-right
save
scissors
strikethrough
subscript
superscript
table
text-height
text-width
th
th-large
th-list
underline
undo
unlink
angle-double-down
angle-double-left
angle-double-right
angle-double-up
angle-down
angle-left
angle-right
angle-up
arrow-circle-down
arrow-circle-left
arrow-circle-o-down
arrow-circle-o-left
arrow-circle-o-right
arrow-circle-o-up
arrow-circle-right
arrow-circle-up
arrow-down
arrow-left
arrow-right
arrow-up
arrows
arrows-alt
arrows-h
arrows-v
caret-down
caret-left
caret-right
caret-square-o-down
caret-square-o-left
caret-square-o-right
caret-square-o-up
caret-up
chevron-circle-down
chevron-circle-left
chevron-circle-right
chevron-circle-up
chevron-down
chevron-left
chevron-right
chevron-up
exchange
hand-o-down
hand-o-left
hand-o-right
hand-o-up
long-arrow-down
long-arrow-left
long-arrow-right
long-arrow-up
toggle-down
toggle-left
toggle-right
toggle-up
arrows-alt
backward
compress
eject
expand
fast-backward
fast-forward
forward
pause
pause-circle
pause-circle-o
play
play-circle
play-circle-o
random
step-backward
step-forward
stop
stop-circle
stop-circle-o
youtube-play
500px
adn
amazon
android
angellist
apple
bandcamp
behance
behance-square
bitbucket
bitbucket-square
bitcoin
black-tie
bluetooth
bluetooth-b
btc
buysellads
cc-amex
cc-diners-club
cc-discover
cc-jcb
cc-mastercard
cc-paypal
cc-stripe
cc-visa
chrome
codepen
codiepie
connectdevelop
contao
css3
dashcube
delicious
deviantart
digg
dribbble
dropbox
drupal
edge
eercast
empire
envira
etsy
expeditedssl
fa
facebook
facebook-f
facebook-official
facebook-square
firefox
first-order
flickr
font-awesome
fonticons
fort-awesome
forumbee
foursquare
free-code-camp
ge
get-pocket
gg
gg-circle
git
git-square
github
github-alt
github-square
gitlab
gittip
glide
glide-g
google
google-plus
google-plus-circle
google-plus-official
google-plus-square
google-wallet
gratipay
grav
hacker-news
houzz
html5
imdb
instagram
internet-explorer
ioxhost
joomla
jsfiddle
lastfm
lastfm-square
leanpub
linkedin
linkedin-square
linode
linux
maxcdn
meanpath
medium
meetup
mixcloud
modx
odnoklassniki
odnoklassniki-square
opencart
openid
opera
optin-monster
pagelines
paypal
pied-piper
pied-piper-alt
pied-piper-pp
pinterest
pinterest-p
pinterest-square
product-hunt
qq
quora
ra
ravelry
rebel
reddit
reddit-alien
reddit-square
renren
resistance
safari
scribd
sellsy
share-alt
share-alt-square
shirtsinbulk
simplybuilt
skyatlas
skype
slack
slideshare
snapchat
snapchat-ghost
snapchat-square
soundcloud
spotify
stack-exchange
stack-overflow
steam
steam-square
stumbleupon
stumbleupon-circle
superpowers
telegram
tencent-weibo
themeisle
trello
tripadvisor
tumblr
tumblr-square
twitch
twitter
twitter-square
usb
viacoin
viadeo
viadeo-square
vimeo
vimeo-square
vine
vk
wechat
weibo
weixin
whatsapp
wikipedia-w
windows
wordpress
wpbeginner
wpexplorer
wpforms
xing
xing-square
y-combinator
y-combinator-square
yahoo
yc
yc-square
yelp
yoast
youtube
youtube-play
youtube-square
warning
ambulance
h-square
heart
heart-o
heartbeat
hospital-o
medkit
plus-square
stethoscope
user-md
wheelchair
wheelchair-alt`.split("\n").map(a => "fa fa-" + a);


const glyphicon = `asterisk
plus
euro
eur
minus
cloud
envelope
pencil
glass
music
search
heart
star
star-empty
user
film
th-large
th
th-list
ok
remove
zoom-in
zoom-out
off
signal
cog
trash
home
file
time
road
download-alt
download
upload
inbox
play-circle
repeat
refresh
list-alt
lock
flag
headphones
volume-off
volume-down
volume-up
qrcode
barcode
tag
tags
book
bookmark
print
camera
font
bold
italic
text-height
text-width
align-left
align-center
align-right
align-justify
list
indent-left
indent-right
facetime-video
picture
map-marker
adjust
tint
edit
share
check
move
step-backward
fast-backward
backward
play
pause
stop
forward
fast-forward
step-forward
eject
chevron-left
chevron-right
plus-sign
minus-sign
remove-sign
ok-sign
question-sign
info-sign
screenshot
remove-circle
ok-circle
ban-circle
arrow-left
arrow-right
arrow-up
arrow-down
share-alt
resize-full
resize-small
exclamation-sign
gift
leaf
fire
eye-open
eye-close
warning-sign
plane
calendar
random
comment
magnet
chevron-up
chevron-down
retweet
shopping-cart
folder-close
folder-open
resize-vertical
resize-horizontal
hdd
bullhorn
bell
certificate
thumbs-up
thumbs-down
hand-right
hand-left
hand-up
hand-down
circle-arrow-right
circle-arrow-left
circle-arrow-up
circle-arrow-down
globe
wrench
tasks
filter
briefcase
fullscreen
dashboard
paperclip
heart-empty
link
phone
pushpin
usd
gbp
sort
sort-by-alphabet
sort-by-alphabet-alt
sort-by-order
sort-by-order-alt
sort-by-attributes
sort-by-attributes-alt
unchecked
expand
collapse-down
collapse-up
log-in
flash
log-out
new-window
record
save
open
saved
import
export
send
floppy-disk
floppy-saved
floppy-remove
floppy-save
floppy-open
credit-card
transfer
cutlery
header
compressed
earphone
phone-alt
tower
stats
sd-video
hd-video
subtitles
sound-stereo
sound-dolby
sound-5-1
sound-6-1
sound-7-1
copyright-mark
registration-mark
cloud-download
cloud-upload
tree-conifer
tree-deciduous
cd
save-file
open-file
level-up
copy
paste
alert
equalizer
king
queen
pawn
bishop
knight
baby-formula
tent
blackboard
bed
apple
erase
hourglass
lamp
duplicate
piggy-bank
scissors
bitcoin
btc
xbt
yen
jpy
ruble
rub
scale
ice-lolly
ice-lolly-tasted
education
option-horizontal
option-vertical
menu-hamburger
modal-window
oil
grain
sunglasses
text-size
text-color
text-background
object-align-top
object-align-bottom
object-align-horizontal
object-align-left
object-align-vertical
object-align-right
triangle-right
triangle-left
triangle-bottom
triangle-top
console
superscript
subscript
menu-left
menu-right
menu-down
menu-up`.split("\n").map(a => "glyphicon glyphicon-" + a);    
