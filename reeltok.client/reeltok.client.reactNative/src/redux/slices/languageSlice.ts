import { createSlice } from '@reduxjs/toolkit'

export type Language = 'da_DK' | 'en_GB'

export interface LanguageProps {
  languages: Language[]
  current: number
}

const initialState: LanguageProps = {
  languages: ['da_DK', 'en_GB'],
  current: 0,
}

const languageSlice = createSlice({
  name: 'language',
  initialState,
  reducers: {
    changeLanguage: (state) => {
      state.current = (state.current + 1) % state.languages.length
    },
  },
})

export const { changeLanguage } = languageSlice.actions
export default languageSlice.reducer

// TODO: Connect the language settings to this slice. Save the language on local storage (for either web and device)
//! I have made an example here, but it is a different setup: https://github.com/MagnusHLund/Resume/blob/main/Web%20client/src/Redux/Slices/LanguageSlice.ts
//! In the example, a button is clicked which changes the language. The example also does not use local storage.
