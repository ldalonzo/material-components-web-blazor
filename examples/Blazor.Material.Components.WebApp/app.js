import './favicon-32.png'
import './app.scss'
import { ApplicationInsights } from '@microsoft/applicationinsights-web'

const appInsights = new ApplicationInsights({ config: {
  instrumentationKey: '2656cbd4-20cb-404e-8a26-af24c943c357'
} })

appInsights.loadAppInsights()
appInsights.trackPageView()
