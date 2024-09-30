//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v14.2.0+1b21caa
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	/// <summary>Img</summary>
	[PublishedModel("img")]
	public partial class Img : PublishedElementModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		public new const string ModelTypeAlias = "img";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<Img, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public Img(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Add Margin Bottom
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("addMarginBottom")]
		public virtual bool AddMarginBottom => this.Value<bool>(_publishedValueFallback, "addMarginBottom");

		///<summary>
		/// Add Margin Top
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("addMarginTop")]
		public virtual bool AddMarginTop => this.Value<bool>(_publishedValueFallback, "addMarginTop");

		///<summary>
		/// Center Image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("centerImage")]
		public virtual bool CenterImage => this.Value<bool>(_publishedValueFallback, "centerImage");

		///<summary>
		/// Image Description
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("imageDescription")]
		public virtual string ImageDescription => this.Value<string>(_publishedValueFallback, "imageDescription");

		///<summary>
		/// Image Width: Enter a positive integer. recommended range: 25-200.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("imageWidth")]
		public virtual int ImageWidth => this.Value<int>(_publishedValueFallback, "imageWidth");

		///<summary>
		/// Rounded Borders
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("roundedBorders")]
		public virtual bool RoundedBorders => this.Value<bool>(_publishedValueFallback, "roundedBorders");

		///<summary>
		/// Section BackGround Color
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sectionBackGroundColor")]
		public virtual global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ColorPickerValueConverter.PickedColor SectionBackGroundColor => this.Value<global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ColorPickerValueConverter.PickedColor>(_publishedValueFallback, "sectionBackGroundColor");

		///<summary>
		/// Selected Image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("selectedImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops SelectedImage => this.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(_publishedValueFallback, "selectedImage");

		///<summary>
		/// Set Img Absolute: Anchors image to first parent element of type section
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgAbsolute")]
		public virtual bool SetImgAbsolute => this.Value<bool>(_publishedValueFallback, "setImgAbsolute");

		///<summary>
		/// Set Img Bottom: anchors img to bottom
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgBottom")]
		public virtual bool SetImgBottom => this.Value<bool>(_publishedValueFallback, "setImgBottom");

		///<summary>
		/// Set Img Left: anchors img to left
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgLeft")]
		public virtual bool SetImgLeft => this.Value<bool>(_publishedValueFallback, "setImgLeft");

		///<summary>
		/// Set Img Right: anchors img to right
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgRight")]
		public virtual bool SetImgRight => this.Value<bool>(_publishedValueFallback, "setImgRight");

		///<summary>
		/// Set Img Top: anchors img to top
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgTop")]
		public virtual bool SetImgTop => this.Value<bool>(_publishedValueFallback, "setImgTop");
	}
}
