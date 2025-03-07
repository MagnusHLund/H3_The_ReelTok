import { useWindowDimensions } from 'react-native'
import { useMemo } from 'react'

// TODO: Use useMemo to increase performance, by not recalculating same result
export default function useAppDimensions() {
  const { height, width } = useWindowDimensions()

  const fullHeight = height
  const fullWidth = width

  const navbarHeight = Math.ceil(height * 0.1)
  const contentHeight = fullHeight - navbarHeight

  return { navbarHeight, contentHeight, fullHeight, fullWidth }
}
