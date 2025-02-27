import { useWindowDimensions } from 'react-native'
import { useMemo } from 'react'

export default function useAppDimensions() {
  const { height } = useWindowDimensions()

  const navbarHeight = height * 0.05
  const contentHeight = height - navbarHeight

  return { navbarHeight, contentHeight }
}
