#!/usr/bin/zsh
cd Binary/Nightly
rm -rf docs*
mdoc update --delete CoreTweet.dll -i CoreTweet.xml CoreTweet.Streaming.Reactive.dll -i CoreTweet.Streaming.Reactive.xml -o ./docs-data/
mdoc export-html -o ./docs/ ./docs-data/
cd docs
sed -i -e "s/Documentation for this section has not yet been entered.//g" **/*.html -u
sed -i -e "s/Untitled/CoreTweet/g" **/*.html -u
