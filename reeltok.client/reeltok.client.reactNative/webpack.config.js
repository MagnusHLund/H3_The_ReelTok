const createExpoWebpackConfigAsync = require('@expo/webpack-config')

module.exports = async function (env, argv) {
  const config = await createExpoWebpackConfigAsync(env, argv)

  // Add XML loader
  config.module.rules.push({
    test: /\.xml$/,
    use: 'xml-loader',
  })

  return config
}
