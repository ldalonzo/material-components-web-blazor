const path = require('path')
const { CleanWebpackPlugin } = require('clean-webpack-plugin')

module.exports = {
  entry: {
    app: ['./app.scss', './app.js'],
    'checkbox': './Checkbox/MDCCheckbox.razor.ts',
    'circular-progress': './CircularProgress/MDCCircularProgress.razor.ts',
    'drawer': './Drawer/MDCDrawer.razor.ts',
    'ripple': './Ripple/MDCRipple.cs.ts',
    'snackbar': './Snackbar/MDCSnackbar.razor.ts',
    'switch': './Switch/MDCSwitch.razor.ts',
    'tab-bar': './TabBar/MDCTabBar.razor.ts',
    'textfield': './TextField/MDCTextField.razor.ts',
    'top-app-bar': './TopAppBar/MDCTopAppBar.razor.ts'
  },
  plugins: [
    new CleanWebpackPlugin()
  ],
  output: {
    filename: '[name].bundle.js',
    path: path.resolve(__dirname, 'wwwroot'),
    publicPath: ""
  },
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: [
          {
            loader: 'file-loader',
            options: {
              name: 'bundle.css'
            }
          },
          'extract-loader',
          'css-loader',
          {
            // Run postcss actions
            loader: 'postcss-loader',
            options: {
              postcssOptions: {
                plugins: [
                  [
                    'autoprefixer'
                  ],
                ],
              },
            }
          },
          {
            // Compiles Sass to CSS
            loader: 'sass-loader',
            options: {
              // Prefer Dart Sass
              implementation: require('sass'),

              // See https://github.com/webpack-contrib/sass-loader/issues/804
              webpackImporter: false,
              sassOptions: {
                includePaths: ['./node_modules']
              }
            }
          }
        ]
      },
      {
        test: /\.js$/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env']
          }
        }
      },
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(png|svg|jpg|gif)$/,
        type: 'asset/resource'
      },
      {
        test: /\.(woff|woff2|eot|ttf|otf)$/,
        use: {
          loader: 'file-loader',
          options: {
            esModule: false,
            name(file) {
              return '[name].[ext]'
            }
          }
        }
      }
    ]
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js']
  }
}
