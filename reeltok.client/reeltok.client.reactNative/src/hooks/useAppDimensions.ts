import { useWindowDimensions } from 'react-native'
import { useMemo } from 'react'

// TODO: Use useMemo to increase performance, by not recalculating same result
export default function useAppDimensions() {
  const { height } = useWindowDimensions()

  const navbarHeight = Math.ceil(height * 0.1)
  const contentHeight = height - navbarHeight

  return { navbarHeight, contentHeight }
}
