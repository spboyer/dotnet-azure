# rename npm package reference in adal-node
# covers package.json and any file referencing package
find ./node_modules/adal-node -type f -exec sed -i '' 's/xpath.js/xpathjs/g' {} \;

# rename the xpath.js npm package folder and name
mv ./node_modules/xpath.js ./node_modules/xpathjs
# change the name in the package.json to reflect naming change
find ./node_modules/xpathjs -type f -exec sed -i '' 's/xpath.js/xpathjs/g' {} \;
