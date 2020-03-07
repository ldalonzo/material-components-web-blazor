import './app.scss'
import { ApplicationInsights } from '@microsoft/applicationinsights-web'

const appInsights = new ApplicationInsights({ config: {
  instrumentationKey: '2430ac1c-7d16-480a-9a1d-08071e3da25a'
} })

appInsights.loadAppInsights()
appInsights.trackPageView()
