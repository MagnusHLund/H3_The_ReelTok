import { PersistGate } from 'redux-persist/integration/react'
import { persistor, store } from './redux/store'
import { Provider } from 'react-redux'
import Router from './Navigation/Router'
import React from 'react'

export default function App() {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <Router />
      </PersistGate>
    </Provider>
  )
}
