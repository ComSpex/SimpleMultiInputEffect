using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SimpleMultiInputEffectShader {
	public class SimpleMultiInputEffect:ShaderEffect {
		static protected Uri[] Uris;
		static protected int indexUris=0;
		#region Constructors
		static SimpleMultiInputEffect() {
			Uri[] uris = {
				Global.MakePackUri("SimpleMultiInputEffect.ps"),
				Global.MakePackUri("DarkenDarkerColors.ps"),
				Global.MakePackUri("LightenLighterColors.ps"),
				Global.MakePackUri("DifferenceSimpleArithmetric.ps"),
				Global.MakePackUri("MuliplySimpleArithmeticMultiplication.ps"),
				Global.MakePackUri("NegativeOfDifference.ps"),
				Global.MakePackUri("OverlayHardlight.ps"),
				Global.MakePackUri("OverlaySoftlight.ps"),
				Global.MakePackUri("Exclusion.ps"),
			};
			Uris=uris;
			_pixelShader.UriSource=Uris[indexUris];
		}
		public Uri CurrentName {
			get { return Uris[indexUris]; }
		}
		public void Next() {
			if(++indexUris>=Uris.Length) {
				indexUris=0;
			}
			_pixelShader.UriSource=Uris[indexUris];
			Update();
		}
		protected void Update() {
			this.PixelShader=_pixelShader;
			// Update each DependencyProperty that's registered with a shader register.  This
			// is needed to ensure the shader gets sent the proper default value.
			UpdateShaderValue(Input1Property);
			UpdateShaderValue(Input2Property);
			UpdateShaderValue(MixInAmountProperty);
		}
		public SimpleMultiInputEffect() {
			Update();
		}
#endregion
#region Dependency Properties
		public Brush Input1 {
			get { return (Brush)GetValue(Input1Property); }
			set { SetValue(Input1Property,value); }
		}
		// Brush-valued properties turn into sampler-property in the shader.
		// This helper sets "ImplicitInput" as the default, meaning the default
		// sampler is whatever the rendering of the element it's being applied to is.
		public static readonly DependencyProperty Input1Property =
				ShaderEffect.RegisterPixelShaderSamplerProperty("Input1",typeof(SimpleMultiInputEffect),0);
		public Brush Input2 {
			get { return (Brush)GetValue(Input2Property); }
			set { SetValue(Input2Property,value); }
		}
		// Brush-valued properties turn into sampler-property in the shader.
		// This helper sets "ImplicitInput" as the default, meaning the default
		// sampler is whatever the rendering of the element it's being applied to is.
		public static readonly DependencyProperty Input2Property =
				ShaderEffect.RegisterPixelShaderSamplerProperty("Input2",typeof(SimpleMultiInputEffect),1);
		public double MixInAmount {
			get { return (double)GetValue(MixInAmountProperty); }
			set { SetValue(MixInAmountProperty,value); }
		}
		// Scalar-valued properties turn into shader constants with the register
		// number sent into PixelShaderConstantCallback().
		public static readonly DependencyProperty MixInAmountProperty =
				DependencyProperty.Register("MixInAmount",typeof(double),typeof(SimpleMultiInputEffect),
								new UIPropertyMetadata(0.5,PixelShaderConstantCallback(0)));
#endregion
#region Member Data
		private static PixelShader _pixelShader = new PixelShader();
#endregion
	}
}
